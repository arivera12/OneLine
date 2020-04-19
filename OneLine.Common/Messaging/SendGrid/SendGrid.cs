using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    public class SendGrid : ISendGridApi
    {
        public ISendGridApiSettings SendGridApiSettings { get; set; }

        public SendGrid(IOptions<SendGridApiSettings> options)
        {
            SendGridApiSettings = options.Value;
        }
        public SendGrid(ISendGridApiSettings sendGridApiSettings)
        {
            SendGridApiSettings = sendGridApiSettings;
        }
        public Task SendEmailAsync(
            EmailAddress from,
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null)
        {
            if (string.IsNullOrWhiteSpace(SendGridApiSettings.SendGridKey))
                throw new ArgumentNullException("The SendGridEmailSenderSettings.SendGridKey is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (from is null)
                throw new ArgumentNullException("The From mail address param is null");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null");

            if(string.IsNullOrWhiteSpace(plainTextContent) && string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentNullException("The plainTextContent and htmlContent params are both null, empty or whitespace");

            var client = new SendGridClient(SendGridApiSettings.SendGridKey, requestHeaders: headers);

            var msg = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent,
                Attachments = attachments
            };

            foreach (var item in toMailAddresses)
            {
                msg.AddTo(item);
            }

            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        public Task SendEmailAsync(
            IEnumerable<EmailAddress> toMailAddresses,
            string subject,
            string plainTextContent,
            string htmlContent,
            List<Attachment> attachments = null,
            Dictionary<string, string> headers = null)
        {
            if (SendGridApiSettings is null)
                throw new ArgumentNullException("The SendGridEmailSenderSettings is null. Check that this config exist in the appSettings.json");

            if (string.IsNullOrWhiteSpace(SendGridApiSettings.SendGridKey))
                throw new ArgumentNullException("The SendGridEmailSenderSettings.SendGridKey is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (string.IsNullOrWhiteSpace(SendGridApiSettings.Email))
                throw new ArgumentNullException("The SendGridEmailSenderSettings.Email is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (string.IsNullOrWhiteSpace(SendGridApiSettings.DisplayName))
                throw new ArgumentNullException("The SendGridEmailSenderSettings.DisplayName is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null.");

            if (string.IsNullOrWhiteSpace(plainTextContent) && string.IsNullOrWhiteSpace(htmlContent))
                throw new ArgumentNullException("The plainTextContent and htmlContent params are both null, empty or whitespace");

            var client = new SendGridClient(SendGridApiSettings.SendGridKey, requestHeaders: headers);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(SendGridApiSettings.Email, SendGridApiSettings.DisplayName),
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent,
                Attachments = attachments
            };

            foreach (var item in toMailAddresses)
            {
                msg.AddTo(item);
            }

            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
