using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;
using TagLib;

namespace BuildingBlocks.MediaSecurity;
public class MediaValidator
{
    private readonly List<string> AllowedExtensions = [
        ".png", ".jpeg", ".jpg",
    ];
    public bool IsMediaValid(IFormFile media)
    {
        var fileExtension = Path.GetExtension(media.FileName);
        if (!AllowedExtensions.Contains(fileExtension)) throw new InternalServerException("Ivalid media type");

        try
        {
            using (var stream = media.OpenReadStream())
            {
                var fileAbstraction = new StreamFileAbstraction(media.FileName, stream, null);
                using (var tagLibFile = TagLib.File.Create(fileAbstraction))
                {
                    var mediaTypes = tagLibFile.Properties.MediaTypes;
                    if ( mediaTypes.HasFlag(MediaTypes.Video) || mediaTypes.HasFlag(MediaTypes.Photo))
                    {
                        return true;
                    }
                    return false;
                }
            }

        }
        catch (Exception)
        {
            throw new InternalServerException("Invalid media");
        }
    }
    public string GetMediaType(IFormFile media)
    {
        if (media.ContentType.StartsWith("image/"))
        {
            return "Image";
        }

        if (media.ContentType.StartsWith("video/"))
        {
            return "Video";
        }
        if (media.ContentType.StartsWith("audio/"))
        {
            return "Audio";
        }
        throw new InternalServerException("Invalid media type");
    }
}


public class StreamFileAbstraction : TagLib.File.IFileAbstraction
{
    private readonly Stream _stream;

    public StreamFileAbstraction(string name, Stream stream, Stream writeStream)
    {
        Name = name;
        _stream = stream;
    }

    public string Name { get; }

    public Stream ReadStream => _stream;

    public Stream WriteStream => throw new NotImplementedException();

    public void CloseStream(Stream stream)
    {
        // Do not close the stream here; it will be closed by the caller
    }
}