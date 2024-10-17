public class Attachment
{
    public string AttachmentId { get; set; }
    public string FileName { get; set; }
    public enum FileType
    {
        Image,
        Video,
        Audio,
        Document,
        Other
    }
    public FileType fileType { get; set; }
    public string Url { get; set; } 

    public Attachment(string attachmentId, string fileName, FileType fileType, string url)
    {
        AttachmentId = attachmentId;
        FileName = fileName;
        this.fileType = fileType;
        Url = url;
    }
}
