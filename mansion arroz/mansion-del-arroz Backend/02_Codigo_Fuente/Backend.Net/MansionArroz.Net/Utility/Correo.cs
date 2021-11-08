using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
namespace MansionArroz.Net.Utility
{
    public class Correo
    {
        public bool EnvioCorreo(string correo, string usuario, string clave)
        {
            try
            {
                MailMessage mail = new MailMessage();
                var fromAddress = new MailAddress("mansiondelarroz@gmail.com", "Mansión del arroz chino");
                var toAddress = new MailAddress(correo);
                string fromPassword = "eoaozxqxxpadarqq";
                string subject = "RECUPERACIÓN DE CLAVE";
                string body = ObtenerPlantilla();
                body = body.Replace("#NombreCompleto#", (usuario));
                body = body.Replace("#NuevaClave#", (clave));

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private string ObtenerPlantilla()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("Utility/Plantillas/plantilla-email.html"))

            {
                body = reader.ReadToEnd();
            }
            return body;
        }
    }

}
