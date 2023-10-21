using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RTools_NTS.Util;
using System;

namespace DangKyPhongThucHanhTruongCNTT.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string? DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class SendMailService
    {
        private readonly MailSettings _settings;

        public SendMailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        Random random = new Random();
        private int token { get; set; }
        public void setToken()
        {
            this.token = random.Next(000010, 999989);
        }
        public int getToken() { return this.token; }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_settings.DisplayName, _settings.Mail);
            message.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            using(var smtp =  new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_settings.Mail, _settings.Password);
                    await smtp.SendAsync(message);
                }
                catch (Exception)
                {
                    Directory.CreateDirectory("MailsSave");
                    var emailsavefile = string.Format(@"MailsSave/{0}.txt", Guid.NewGuid());
                    await message.WriteToAsync(emailsavefile);
                }
                await smtp.DisconnectAsync(true);
            }
            
        }
    }
}
