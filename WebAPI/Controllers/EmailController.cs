using System;
using System.Net;
using System.Net.Mail;

namespace WebAPI.Controllers
{
    public class EmailService
    {
        private readonly string _senderEmail;
        private readonly string _applicationName;
        private readonly string _applicationPassword;

        public EmailService()
        {
            _senderEmail = "scerdasb@ucenfotec.ac.cr";
            _applicationName = "GymProject";
            _applicationPassword = "tifi gghs ivxc fwkd"; 
        }

        public bool SendEmailWithOtp(string recipientEmail, string otp)
        {
            try
            {
                using (var msg = new MailMessage())
                {
                    msg.Subject = "Tu código de verificación para registro en el gimnasio";
                    msg.Body = $"Tu código de verificación es: {otp}. Es válido por 15 minutos.";
                    msg.From = new MailAddress(_senderEmail);
                    msg.To.Add(recipientEmail);

                    using (var client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new System.Net.NetworkCredential(_senderEmail, _applicationPassword);

                        client.Send(msg);
                    }
                }

                Console.WriteLine($"Email enviado exitosamente a {recipientEmail}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el email: {ex.Message}");
                return false; // Devuelve false si hubo un error al enviar el correo
            }
        }

    }
}