using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    /// <summary>
    /// Defines send grid api service
    /// </summary>
    public interface ISendGridApi
    {
        /// <summary>
        ///  The send grid api settings
        /// </summary>
        ISendGridApiSettings SendGridApiSettings { get; set; }
        /// <summary>
        /// Send email asyncronously using the send grid api
        /// </summary>
        /// <param name="from">The sender email address</param>
        /// <param name="toMailAddresses">The recipient's email address/es</param>
        /// <param name="subject">The email subject</param>
        /// <param name="plainTextContent">The email content as plain text</param>
        /// <param name="htmlContent">The email content as html content</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
        /// <returns></returns>
        Task SendEmailAsync(
            EmailAddress from,
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
        /// <summary>
        /// Send email asyncronously using the send grid api
        /// </summary>
        /// <param name="from">The sender email address</param>
        /// <param name="toMailAddresses">The recipient's email address/es</param>
        /// <param name="subject">The email subject</param>
        /// <param name="plainTextContent">The email content as plain text</param>
        /// <param name="htmlContent">The email content as html content</param>
        /// <param name="attachments">The email attachments</param>
        /// <param name="headers">The email headers</param>
        /// <returns></returns>
        Task SendEmailAsync(
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
    }
}
