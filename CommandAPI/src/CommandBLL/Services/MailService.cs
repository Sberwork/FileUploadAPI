using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommandBLL.Services
{
    public class MailService
    {

        public static void SendMail(string bodyMessage)
        {
            var fromAddress = new MailAddress("boyirjon@list.ru");
            var fromPassword = "b89773394";
            var toAddress = new MailAddress("halimjonik@mail.ru");

            string subject = "Hangfire Рассылка";
            string body = bodyMessage;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.Attachments.Add(new Attachment("D://Senat_Backend_Task.docx"));
            smtp.Send(message);
        }
    }
}
