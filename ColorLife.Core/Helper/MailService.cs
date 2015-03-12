using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
namespace ColorLife.Core.Helper
{
    public interface IMailService
    {
        bool Send();
        bool Send(string toEmail, string subject, string body, string smtpUserName, string smtpPassword, string smtpHost, int smtpPort);
    }
    public class MailService:IMailService
    {
        public MailMessage MailMessage { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string[] ToEmail{get;set;}
        public MailAddress MailAddress { get; set; }
        public MailAddressCollection MailAddressCollection { get; set; } 
        public  bool Send()
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = this.EnableSsl;
                    smtpClient.Host = this.SmtpHost;
                    smtpClient.Port = this.SmtpPort;
                    smtpClient.UseDefaultCredentials = this.UseDefaultCredentials;
                    smtpClient.Credentials = new NetworkCredential(this.SmtpUserName, this.SmtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(this.FromEmail, this.FromName),
                        Subject = this.Subject,
                        Body = this.Body,
                        Priority = MailPriority.Normal,
                        Sender = new MailAddress(this.FromEmail),                             
                    };
                    foreach (var item in this.ToEmail)
                    {
                        msg.To.Add(item);
                    }
                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public bool Send(string toEmail, string subject, string body, string smtpUserName, string smtpPassword, string smtpHost, int smtpPort)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };

                    msg.To.Add(toEmail);

                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
