namespace LifeMastery.Infrastructure.Options;

public sealed class EmailProviderOptions
{
    public string ImapHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
