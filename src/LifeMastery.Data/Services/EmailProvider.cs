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

    public async Task<EmailMessage[]> GetMessages(string sender, CancellationToken cancellationToken)
    {
        using var client = new ImapClient();

        await client.ConnectAsync(options.ImapHost, options: SecureSocketOptions.SslOnConnect, cancellationToken: cancellationToken);
        await client.AuthenticateAsync(options.UserName, options.Password, cancellationToken);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly, cancellationToken);

        var uids = await inbox.SearchAsync(SearchQuery.FromContains(sender), cancellationToken);

        var emailMessages = ParseMessages(inbox, uids).ToArray();

        await client.DisconnectAsync(quit: true, cancellationToken: cancellationToken);

        return emailMessages;
    }

    public async Task RemoveMessages(string sender, CancellationToken cancellationToken)
    {
        using var client = new ImapClient();

        await client.ConnectAsync(options.ImapHost, options: SecureSocketOptions.SslOnConnect, cancellationToken: cancellationToken);
        await client.AuthenticateAsync(options.UserName, options.Password, cancellationToken);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadWrite, cancellationToken);

        var uids = await inbox.SearchAsync(SearchQuery.FromContains(sender), cancellationToken);

        foreach (var uid in uids)
        {
            inbox.AddFlags(uid, MessageFlags.Deleted, silent: true, cancellationToken: cancellationToken);
        }

        await inbox.ExpungeAsync(cancellationToken);

        await client.DisconnectAsync(quit: true, cancellationToken: cancellationToken);
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
