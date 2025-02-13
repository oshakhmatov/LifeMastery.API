namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class EmailMessage
{
    public required string Subject { get; init; }
    public required string Content { get; init; }
}
