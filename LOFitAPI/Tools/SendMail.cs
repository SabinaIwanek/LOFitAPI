using System.Net.Mail;
using System.Net;

namespace LOFitAPI.Tools
{
    public static class SendMail
    {
        public static string SmtpLogin = "dowysylki2@outlook.com";
        public static string SmtpPassword = "Wysylka1234567890";
        public static string SmtpHost = "smtp-mail.outlook.com";
        public static int SmtpPort = 587;
        public static string Tittle = "LOFit - odzyskiwanie hasła.";

        public static string Send(string sendTo, int code)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(SmtpHost, SmtpPort)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(SmtpLogin, SmtpPassword)
                };

                MailAddress from = new MailAddress(SmtpLogin, Tittle);
                MailAddress to = new MailAddress(sendTo);

                string body = $"Oto kod do zmiany hasła: {code}. Kod jest ważny przez 15 minut.";

                MailMessage myMail = new MailMessage(from, to)
                {
                    Subject = Tittle,
                    Body = body
                };

                smtp.Send(myMail);
                return "Ok";
            }
            catch(Exception ex) 
            { return ex.ToString(); }
        }
    }
}
