using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OneLine.Messaging
{
    public class Smtp : ISmtp
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ISmtpSettings SmtpSettings { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Smtp()
        {

        }
        /// <summary>
        /// Constructor using DI
        /// </summary>
        /// <param name="options"></param>
        public Smtp(IOptions<SmtpSettings> options)
        {
            SmtpSettings = options.Value;
        }
        /// <summary>
        /// Constructor using DI
        /// </summary>
        /// <param name="options"></param>
        public Smtp(ISmtpSettings smtpSettings)
        {
            SmtpSettings = smtpSettings;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SendEmail(
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
            IDictionary<string, string> headers = null)
        {
            if (from is null)
                throw new ArgumentNullException("The From mail address param is null");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null");

            var mailMessage = new MailMessage
            {
                From = from
            };

            foreach (var item in toMailAddresses)
            {
                mailMessage.To.Add(new MailAddress(item.Address, item.DisplayName));
            }

            if (replyToMailAddresses != null && replyToMailAddresses.Any())
            {
                foreach (var item in replyToMailAddresses)
                {
                    mailMessage.ReplyToList.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesBcc != null && mailAddressesBcc.Any())
            {
                foreach (var item in mailAddressesBcc)
                {
                    mailMessage.Bcc.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesCc != null && mailAddressesCc.Any())
            {
                foreach (var item in mailAddressesCc)
                {
                    mailMessage.CC.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = IsBodyHtml;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    mailMessage.Headers.Add(header.Key, header.Value);
                }
            }

            if (attachments != null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(item);
                }
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = SmtpSettings.UseDefaultCredentials;
                smtpClient.Host = SmtpSettings.Host;
                smtpClient.Port = SmtpSettings.Port;
                smtpClient.EnableSsl = SmtpSettings.EnableSsl;
                smtpClient.Credentials = SmtpSettings.UseDefaultCredentials ?
                                            CredentialCache.DefaultNetworkCredentials :
                                            new NetworkCredential(SmtpSettings.UserName, SmtpSettings.Password);
                smtpClient.Timeout = SmtpSettings.Timeout;
                mailMessage.Priority = MailPriority;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SendEmail(
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null)
        {
            if (SmtpSettings is null)
                throw new ArgumentNullException("The SmtpSettings is null. Check that this config exist in the appSettings.json");

            if (string.IsNullOrWhiteSpace(SmtpSettings.Email))
                throw new ArgumentNullException("The SmtpSettings.Email is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null");

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException("The body is null, empty or whitespace.");

            var mailMessage = new MailMessage
            {
                From = string.IsNullOrWhiteSpace(SmtpSettings.DisplayName) ?
                new MailAddress(SmtpSettings.Email) :
                new MailAddress(SmtpSettings.Email, SmtpSettings.DisplayName)
            };

            foreach (var item in toMailAddresses)
            {
                mailMessage.To.Add(new MailAddress(item.Address, item.DisplayName));
            }

            if (replyToMailAddresses != null && replyToMailAddresses.Any())
            {
                foreach (var item in replyToMailAddresses)
                {
                    mailMessage.ReplyToList.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesBcc != null && mailAddressesBcc.Any())
            {
                foreach (var item in mailAddressesBcc)
                {
                    mailMessage.Bcc.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesCc != null && mailAddressesCc.Any())
            {
                foreach (var item in mailAddressesCc)
                {
                    mailMessage.CC.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = IsBodyHtml;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    mailMessage.Headers.Add(header.Key, header.Value);
                }
            }

            if (attachments != null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(item);
                }
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = SmtpSettings.UseDefaultCredentials;
                smtpClient.Host = SmtpSettings.Host;
                smtpClient.Port = SmtpSettings.Port;
                smtpClient.EnableSsl = SmtpSettings.EnableSsl;
                smtpClient.Credentials = SmtpSettings.UseDefaultCredentials ?
                                            CredentialCache.DefaultNetworkCredentials :
                                            new NetworkCredential(SmtpSettings.UserName, SmtpSettings.Password);
                smtpClient.Timeout = SmtpSettings.Timeout;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.Priority = MailPriority;
                smtpClient.Send(mailMessage);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task SendEmailAsync(
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
            IDictionary<string, string> headers = null)
        {
            if (from is null)
                throw new ArgumentNullException("The From mail address param is null");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null");

            var mailMessage = new MailMessage
            {
                From = from
            };

            foreach (var item in toMailAddresses)
            {
                mailMessage.To.Add(new MailAddress(item.Address, item.DisplayName));
            }

            if (replyToMailAddresses != null && replyToMailAddresses.Any())
            {
                foreach (var item in replyToMailAddresses)
                {
                    mailMessage.ReplyToList.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesBcc != null && mailAddressesBcc.Any())
            {
                foreach (var item in mailAddressesBcc)
                {
                    mailMessage.Bcc.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesCc != null && mailAddressesCc.Any())
            {
                foreach (var item in mailAddressesCc)
                {
                    mailMessage.CC.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = IsBodyHtml;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    mailMessage.Headers.Add(header.Key, header.Value);
                }
            }

            if (attachments != null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(item);
                }
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = SmtpSettings.UseDefaultCredentials;
                smtpClient.Host = SmtpSettings.Host;
                smtpClient.Port = SmtpSettings.Port;
                smtpClient.EnableSsl = SmtpSettings.EnableSsl;
                smtpClient.Credentials = SmtpSettings.UseDefaultCredentials ?
                                            CredentialCache.DefaultNetworkCredentials :
                                            new NetworkCredential(SmtpSettings.UserName, SmtpSettings.Password);
                smtpClient.Timeout = SmtpSettings.Timeout;
                mailMessage.Priority = MailPriority;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task SendEmailAsync(
            IList<MailAddress> toMailAddresses,
            string subject,
            string body,
            bool IsBodyHtml = true,
            MailPriority MailPriority = MailPriority.Normal,
            IList<MailAddress> replyToMailAddresses = null,
            IList<MailAddress> mailAddressesBcc = null,
            IList<MailAddress> mailAddressesCc = null,
            IList<Attachment> attachments = null,
            IDictionary<string, string> headers = null)
        {
            if (SmtpSettings is null)
                throw new ArgumentNullException("The SmtpSettings is null. Check that this config exist in the appSettings.json");

            if (string.IsNullOrWhiteSpace(SmtpSettings.Email))
                throw new ArgumentNullException("The SmtpSettings.Email is null, empty or whitespace. Check that this config exist in the appSettings.json");

            if (toMailAddresses is null || !toMailAddresses.Any())
                throw new ArgumentNullException("The To mail addresses param is null");

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentNullException("The body is null, empty or whitespace.");

            var mailMessage = new MailMessage
            {
                From = string.IsNullOrWhiteSpace(SmtpSettings.DisplayName) ?
                new MailAddress(SmtpSettings.Email) :
                new MailAddress(SmtpSettings.Email, SmtpSettings.DisplayName)
            };

            foreach (var item in toMailAddresses)
            {
                mailMessage.To.Add(new MailAddress(item.Address, item.DisplayName));
            }

            if (replyToMailAddresses != null && replyToMailAddresses.Any())
            {
                foreach (var item in replyToMailAddresses)
                {
                    mailMessage.ReplyToList.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesBcc != null && mailAddressesBcc.Any())
            {
                foreach (var item in mailAddressesBcc)
                {
                    mailMessage.Bcc.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            if (mailAddressesCc != null && mailAddressesCc.Any())
            {
                foreach (var item in mailAddressesCc)
                {
                    mailMessage.CC.Add(new MailAddress(item.Address, item.DisplayName));
                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = IsBodyHtml;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    mailMessage.Headers.Add(header.Key, header.Value);
                }
            }

            if (attachments != null && attachments.Any())
            {
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(item);
                }
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = SmtpSettings.UseDefaultCredentials;
                smtpClient.Host = SmtpSettings.Host;
                smtpClient.Port = SmtpSettings.Port;
                smtpClient.EnableSsl = SmtpSettings.EnableSsl;
                smtpClient.Credentials = SmtpSettings.UseDefaultCredentials ?
                                            CredentialCache.DefaultNetworkCredentials :
                                            new NetworkCredential(SmtpSettings.UserName, SmtpSettings.Password);
                smtpClient.Timeout = SmtpSettings.Timeout;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.Priority = MailPriority;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}

