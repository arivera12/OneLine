using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    public interface ISendGridApi
    {
        ISendGridApiSettings SendGridApiSettings { get; set; }
        Task SendEmailAsync(
            EmailAddress from,
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
        Task SendEmailAsync(
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null);
    }
}
