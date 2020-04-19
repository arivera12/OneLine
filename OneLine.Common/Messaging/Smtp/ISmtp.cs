using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    public interface ISmtp
    {
        ISmtpSettings SmtpSettings { get; set; }
        void SendEmail(
            MailAddress from,
            List<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            List<MailAddress> replyToMailAddresses = null,
            List<MailAddress> mailAddressesBcc = null,
            List<MailAddress> mailAddressesCc = null,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
        void SendEmail(
            List<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            List<MailAddress> replyToMailAddresses = null,
            List<MailAddress> mailAddressesBcc = null,
            List<MailAddress> mailAddressesCc = null,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
        Task SendEmailAsync(
            MailAddress from,
            List<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            List<MailAddress> replyToMailAddresses = null,
            List<MailAddress> mailAddressesBcc = null,
            List<MailAddress> mailAddressesCc = null,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
        Task SendEmailAsync(
            List<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            List<MailAddress> replyToMailAddresses = null,
            List<MailAddress> mailAddressesBcc = null,
            List<MailAddress> mailAddressesCc = null,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
    }
}
