namespace ELibrary.Services.CommonResurcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.CommonResurcesServices;

    public class SendMail : ISendMail
    {
        private readonly string fromEmail = "elibrarysite2019@gmail.com";
        private readonly string password = "Elibrary2019!";
        private readonly ApplicationDbContext context;

        public SendMail(ApplicationDbContext context)
        {
            this.context = context;
        }



        public bool SendingMail(string toMail, string subject, string messageBody)
        {
            try
            {
                // Create smtp connection.
                SmtpClient client = new SmtpClient();

                // outgoing port for the mail.
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(this.fromEmail, this.password);

                // Fill the mail form.
                var send_mail = new MailMessage();
                send_mail.IsBodyHtml = true;

                // address from where mail will be sent.
                send_mail.From = new MailAddress(this.fromEmail);

                // address to which mail will be sent.
                send_mail.To.Add(new MailAddress(toMail));

                // subject and body of the mail.
                send_mail.Subject = subject;
                send_mail.Body = messageBody;

                client.Send(send_mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SendMailByTemplate(string toMail, string templateName, Dictionary<string, string> info)
        {
            Dictionary<string, string> templateResult = new Dictionary<string, string>();
            switch (templateName)
            {
                case "VerifyMailTemplate":
                    templateResult = this.VerifyMailTemplate(info["code"]);
                    break;
                case "ForgotenPasswordSendCode":
                    templateResult = this.ForgotenPasswordSendCode(info["code"]);
                    break;
                case "NewRegesterUser":
                    templateResult = this.NewRegesterUser(toMail, info["password"], info["code"]);
                    break;
                case "NewMessage":
                    templateResult = this.NewMessage(toMail, info["message"]);
                    break;
                default:
                    break;
            }

            var subject = templateResult["Subject"];
            var message = templateResult["Message"];
            this.SendingMail(toMail, subject, message);
        }

        public Dictionary<string, string> VerifyMailTemplate(string url)
        {
            var result = new Dictionary<string, string>();
            result.Add("Subject", "Email за потвърждение на акаунт");
            StringBuilder ms = new StringBuilder();
            ms.AppendLine("Успешно регистриран email в системата на Elibrary<br/>");
            ms.AppendLine($"Код за потвърждение: {url}<br/>");
            result.Add("Message", ms.ToString().Trim());
            return result;
        }

        public Dictionary<string, string> ForgotenPasswordSendCode(string code)
        {
            var result = new Dictionary<string, string>();
            result.Add("Subject", "Email за забравена парола");
            StringBuilder ms = new StringBuilder();
            ms.AppendLine($"Моля използвайте този код: {code}<br/>");
            result.Add("Message", ms.ToString().Trim());
            return result;
        }

        public Dictionary<string, string> NewRegesterUser(string email, string password, string code)
        {
            var result = new Dictionary<string, string>();
            result.Add("Subject", "Успешна регистрация");
            StringBuilder ms = new StringBuilder();
            ms.AppendLine($"Вие сте регистриран успешно с email: {email}<br/>");
            ms.AppendLine($"Паролата ви: {password}<br/>");
            ms.AppendLine($"Моля използвайте този код: {code}<br/>");
            result.Add("Message", ms.ToString().Trim());
            return result;
        }

        public Dictionary<string, string> NewMessage(string email, string message)
        {
            var result = new Dictionary<string, string>();
            result.Add("Subject", "Успешна регистрация");
            StringBuilder ms = new StringBuilder();
            ms.AppendLine($"Има ново известие в профила ви в ELibrary: {email}<br/>");
            ms.AppendLine(message);

            result.Add("Message", ms.ToString().Trim());
            return result;
        }
    }
}
