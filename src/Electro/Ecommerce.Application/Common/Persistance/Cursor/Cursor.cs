using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Persistance.Cursor;
public sealed record Cursor(DateTime Date, Guid LastId)
{
    public static string Encode(DateTime date, Guid lastId)
    {
        var cursor = new Cursor(date, lastId);
        string json = JsonConvert.SerializeObject(cursor);
        return Base64UrlTextEncoder.Encode(Encoding.UTF8.GetBytes(json));
    }

    public static Cursor Decode(string cursor)
    {
        if (string.IsNullOrWhiteSpace(cursor)) return null;

        try
        {
            string json = Encoding.UTF8.GetString(Base64UrlTextEncoder.Decode(cursor));
            return JsonConvert.DeserializeObject<Cursor>(json);
        }
        catch (Exception ex)
        {
            return null;
        }

    }
}