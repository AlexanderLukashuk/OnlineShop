using System;
using System.Net;
using System.Net.Mail;

namespace OnlineShop.MessageSenderService
{
    public class MailSender
    {
        public void SendMail(string email)
        {
            MailAddress from = new MailAddress("upiter465@gmail.com");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Аутентикация/Авторизация";
            message.Body = "Аутентикация/Авторизация прошла успешно";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //smtp.Credentials = new NetworkCredential()
            smtp.EnableSsl = true;
            smtp.Send(message);
        }
    }
}