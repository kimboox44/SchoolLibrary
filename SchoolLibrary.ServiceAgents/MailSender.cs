namespace SchoolLibrary.ServiceAgents
{
    using System.Net;
    using System.Net.Mail;

    public class MailSender
    {
        private SmtpClient smtp;

        public MailSender()
        {
            this.smtp = new SmtpClient();
            this.smtp.Host = "smtp.gmail.com";
            this.smtp.Port = 587;

            this.smtp.Credentials = new NetworkCredential(
                "school.library098@gmail.com", "qwerty123$$");
            this.smtp.EnableSsl = true;
        }

        public void Send(string email, string subject, string text)
        {
            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress("school.library098@gmail.com");
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = text;

            this.smtp.Send(mail);
        }
    }
}