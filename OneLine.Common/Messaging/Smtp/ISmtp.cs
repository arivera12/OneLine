using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    /// <summary>
    /// Defines a methods to send emails using smpt client
    /// </summary>
    public interface ISmtp
    {
        /// <summary>
        /// The Smtp settings
        /// </summary>
        ISmtpSettings SmtpSettings { get; set; }
        /// <summary>
        /// Send mail using smtp client
        /// </summary>
        /// <param name="from">The mail sender address</param>
        /// <param name="toMailAddresses">The recipient's mail's address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The body of the email</param>
        /// <param name="IsBodyHtml">The body is html indicator</param>
        /// <param name="MailPriority">The mail priority</param>
        /// <param name="replyToMailAddresses">Reply mail's recipients address</param>
        /// <param name="mailAddressesBcc">Black carbon copy recipients mail address</param>
        /// <param name="mailAddressesCc">Carbon copy recipients mail address</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
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
        /// <summary>
        /// Send mail using smtp client
        /// </summary>
        /// <param name="toMailAddresses">The recipient's mail's address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The body of the email</param>
        /// <param name="IsBodyHtml">The body is html indicator</param>
        /// <param name="MailPriority">The mail priority</param>
        /// <param name="replyToMailAddresses">Reply mail's recipients address</param>
        /// <param name="mailAddressesBcc">Black carbon copy recipients mail address</param>
        /// <param name="mailAddressesCc">Carbon copy recipients mail address</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
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
        /// <summary>
        /// Send mail async using smtp client
        /// </summary>
        /// <param name="from">The mail sender address</param>
        /// <param name="toMailAddresses">The recipient's mail's address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The body of the email</param>
        /// <param name="IsBodyHtml">The body is html indicator</param>
        /// <param name="MailPriority">The mail priority</param>
        /// <param name="replyToMailAddresses">Reply mail's recipients address</param>
        /// <param name="mailAddressesBcc">Black carbon copy recipients mail address</param>
        /// <param name="mailAddressesCc">Carbon copy recipients mail address</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
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
        /// <summary>
        /// Send mail async using smtp client
        /// </summary>
        /// <param name="toMailAddresses">The recipient's mail's address</param>
        /// <param name="subject">The email subject</param>
        /// <param name="body">The body of the email</param>
        /// <param name="IsBodyHtml">The body is html indicator</param>
        /// <param name="MailPriority">The mail priority</param>
        /// <param name="replyToMailAddresses">Reply mail's recipients address</param>
        /// <param name="mailAddressesBcc">Black carbon copy recipients mail address</param>
        /// <param name="mailAddressesCc">Carbon copy recipients mail address</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
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
