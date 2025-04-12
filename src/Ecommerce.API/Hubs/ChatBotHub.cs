using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Ecommerce.API.Hubs;

public class ChatBotHub : Hub
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static readonly string _apiKey = "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";
    private static readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";
    private readonly IDistributedCache _cache;

    public ChatBotHub(IDistributedCache cache)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        _cache = cache;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("WelcomeMessage", "Welcome to AI Chatbot");
    }

    public async Task SendMessageToAI(string user, string message)
    {
        List<Message> conversation = await GetConversationFromCacheAsync(user);

        conversation.Add(new Message { Role = "user", Content = message });
        await Clients.Caller.SendAsync("StartAIStream", "ElectroAI");

        await foreach (string token in StreamChatGptResponseAsync(conversation))
        {
            await Clients.Caller.SendAsync("ReceiveAIMessageStream", "ElectroAI", token);
        }

        conversation.Add(new Message { Role = "assistant", Content = "[COMPLETED]" });
        await SaveConversationToCacheAsync(user, conversation);
    }

    private async Task<List<Message>> GetConversationFromCacheAsync(string user)
    {
        string? json = await _cache.GetStringAsync(user);
        if (string.IsNullOrEmpty(json))
        {
            return new List<Message>
            {
                new Message { Role = "system", Content = "You are a helpful and friendly AI assistant." }
            };
        }
        return JsonConvert.DeserializeObject<List<Message>>(json);
    }

    private async Task SaveConversationToCacheAsync(string user, List<Message> conversation)
    {
        try
        {
            string json = JsonConvert.SerializeObject(conversation);
            await _cache.SetStringAsync(user, json);
        }
        catch (Exception) { }
    }

    private async IAsyncEnumerable<string> StreamChatGptResponseAsync(List<Message> conversation)
    {
        var requestBody = new
        {
            model = "gpt-4o",
            messages = conversation,
            max_tokens = 1000,
            temperature = 0.7,
            top_p = 1.0,
            frequency_penalty = 0,
            presence_penalty = 0,
            stream = true
        };

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _apiUrl)
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        using Stream stream = await response.Content.ReadAsStreamAsync();
        using StreamReader reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            string? line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("data: ")) continue;

            string json = line.Substring(6);
            if (json.Trim() == "[DONE]") yield break;

            OpenAIStreamChunk? chunk = JsonConvert.DeserializeObject<OpenAIStreamChunk>(json);
            string? token = chunk?.Choices?[0]?.Delta?.Content;

            if (!string.IsNullOrEmpty(token))
                yield return token;
        }
    }
}

public class Message
{
    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }
}

public class OpenAIStreamChunk
{
    [JsonProperty("choices")]
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    [JsonProperty("delta")]
    public Delta Delta { get; set; }
}

public class Delta
{
    [JsonProperty("content")]
    public string Content { get; set; }
}
