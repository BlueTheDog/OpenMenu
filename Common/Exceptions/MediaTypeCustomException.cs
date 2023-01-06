namespace Common.Exceptions;
public class MediaTypeCustomException : Exception
{
    public string Type { get; set; } = "media-type-exception";
    public string Detail { get; set; } = "You must provide a media type as an accept header.";
    public string Title { get; set; } = "Media-Type Exception";
    public MediaTypeCustomException()
    {
    }
}
