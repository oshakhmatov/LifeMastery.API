namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class EmailMessage
{
    public string Subject { get; set; }
    public string[] AttachmentContents { get; set; }
}
