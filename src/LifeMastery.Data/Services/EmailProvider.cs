using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using LifeMastery.Infrastructure.Options;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;

namespace LifeMastery.Infrastructure.Services;

public class EmailProvider : IEmailProvider
{
    private readonly EmailProviderOptions options;

    public EmailProvider(IOptionsSnapshot<EmailProviderOptions> optionsSnapshot)
    {
        options = optionsSnapshot.Value;
    }

    public async Task<EmailMessage[]> GetMessages(string subject)
    {
        using var client = new ImapClient();

        await client.ConnectAsync(options.ImapHost, options: SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync(options.UserName, options.Password);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        var uids = await inbox.SearchAsync(SearchQuery.SubjectContains(subject));

        var emailMessages = ParseMessages(inbox, uids).ToArray();

        await client.DisconnectAsync(quit: true);

        return emailMessages;
    }

    public async Task RemoveMessages(string subject)
    {
        using var client = new ImapClient();

        await client.ConnectAsync(options.ImapHost, options: SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync(options.UserName, options.Password);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadWrite);

        var uids = await inbox.SearchAsync(SearchQuery.SubjectContains(subject));

        foreach (var uid in uids)
        {
            inbox.AddFlags(uid, MessageFlags.Deleted, silent: true);
        }

        await inbox.ExpungeAsync();

        await client.DisconnectAsync(quit: true);
    }

    static IEnumerable<EmailMessage> ParseMessages(IMailFolder inbox, IList<UniqueId> messageIds)
    {
        foreach (var messageId in messageIds)
        {
            var message = inbox.GetMessage(messageId);

            yield return new EmailMessage
            {
                Subject = message.Subject,
                AttachmentContents = DecodeAttachmentsToStrings(message.Attachments).ToArray()
            };
        }
    }

    static IEnumerable<string> DecodeAttachmentsToStrings(IEnumerable<MimeEntity> attachments)
    {
        foreach (var attachment in attachments)
        {
            if (attachment is MimePart part)
            {
                yield return DecodeAttachmentToString(part);
            }
        }
    }

    static string DecodeAttachmentToString(MimePart attachment)
    {
        using var memoryStream = new MemoryStream();

        attachment.Content.DecodeTo(memoryStream);

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}
