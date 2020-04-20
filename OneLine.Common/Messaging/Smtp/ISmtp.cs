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
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null);
        void SendEmail(
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null);
        Task SendEmailAsync(
            MailAddress from,
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null);
        Task SendEmailAsync(
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null);
    }
}
