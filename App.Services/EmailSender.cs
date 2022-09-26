using App.Services.Contracts;
using App.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IWritableOptions<SiteSettings> _writableLocations;
        public EmailSender(IWritableOptions<SiteSettings> writableLocations)
        {
            _writableLocations = writableLocations;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            string UserName = _writableLocations.Value.EmailSetting.Username;
            string Password = _writableLocations.Value.EmailSetting.Password;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mohassabaf@gmail.com", subject);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(UserName, Password);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
           
        
            await Task.CompletedTask;
        }



        //public async Task SendEmailAsync(string email, string subject, string message)
        //{
        //    using (var Client = new SmtpClient())
        //    {
        //        var Credential = new NetworkCredential
        //        {
        //            UserName = _writableLocations.Value.EmailSetting.Username,
        //            Password = _writableLocations.Value.EmailSetting.Password,
        //        };

        //        Client.Credentials = Credential;
        //        Client.Host = _writableLocations.Value.EmailSetting.Host;
        //        Client.Port = _writableLocations.Value.EmailSetting.Port;
        //        Client.EnableSsl = true;

        //        using (var emailMessage = new MailMessage())
        //        {
        //            emailMessage.To.Add(new MailAddress(email));
        //            emailMessage.From = new MailAddress(_writableLocations.Value.EmailSetting.Email);
        //            emailMessage.Subject = subject;
        //            emailMessage.IsBodyHtml = true;
        //            emailMessage.Body = message;

        //            Client.Send(emailMessage);
        //        };

        //        await Task.CompletedTask;
        //    }
        // }
    }
}
