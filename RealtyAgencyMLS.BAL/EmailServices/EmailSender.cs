using Microsoft.Extensions.Options;
using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmal(EmailTemplates template, MailRequest mailRequest)
        {
            try
            {
                // Address
                string strFrom = _mailSettings.Mail;

                // Sender
                MailAddress from = new MailAddress(strFrom, "Reality Agency", Encoding.UTF8);

                // Message
                MailMessage message = new MailMessage { From = from };

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailRequest.Body, null, "text/html");
                message.AlternateViews.Add(htmlView);

                message.To.Add(new MailAddress(_mailSettings.IsTest ? _mailSettings.TestEmail : mailRequest.ToEmail, mailRequest.DisplayName, Encoding.UTF8));


                // Subject and Body
                message.Subject = mailRequest.Subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                // SMTP
                SmtpClient client = new SmtpClient(_mailSettings.Host);

                // Port
                if (!string.IsNullOrWhiteSpace(_mailSettings.Port.ToString()))
                {
                    client.Port = _mailSettings.Port;
                }

                // SSL
                client.EnableSsl = _mailSettings.EnableSsl;

                if (string.IsNullOrWhiteSpace(_mailSettings.Password) || string.IsNullOrWhiteSpace(_mailSettings.Password))
                {
                    client.UseDefaultCredentials = true;
                }
                else
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                }

                // Send
                await client.SendMailAsync(message);
            }
            catch (Exception) { }

            return true;
        }
    }
}
