using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using LasEmpanadas.Models;

namespace LasEmpanadas.Services
{
    public class EmailService
    {
        internal int SendEmail(string email, Guid token)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("lasempanadas.empanadas@gmail.com",
               "@Test1234");
            try
            {
            smtp.Send("lasempanadas.empanadas@gmail.com", email,
               "Nueva invitacion a pedido", "http://localhost:52521/Pedido/Elegir/"+token.ToString());
                return 1;
            } catch (Exception)
            {
                return 0;
            }
        }
    }
}