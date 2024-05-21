using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Sneat.DAL.Entity;
using Sneat.PL.Setting;

namespace Sneat.PL.Helper
{
    public class EmailSettings : IMailSetting
    {
        private MailSetting _options;

        public EmailSettings(IOptions<MailSetting> options)
        {
            _options = options.Value;
        }
        public void SendMail(Email email)
        {
            var Mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject,
            };
            Mail.To.Add(MailboxAddress.Parse(email.To));
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            Mail.Body = builder.ToMessageBody();
            Mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

           using var smtp = new SmtpClient();
            smtp.Connect(_options.Host,_options.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email,_options.Password);
            smtp.Send(Mail);
            smtp.Disconnect(true);
        }
    }
}
