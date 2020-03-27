using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewsWorldwide.Controllers
{
    public class EmailController : Controller
    {
        public async Task<IActionResult> SendEmail(string fromEmail, string subject, string message, string name)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("dancho.development@gmail.com", "DanchoKapitancho1234");
            //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("dancho.development@gmail.com", "News Web Site");
            mail.To.Add(new MailAddress("jordang.varna@gmail.com"));
            //mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));
            mail.Subject = $"News Web Site Contact on: \"{subject}\"";
            mail.Body = $"From: {name};\n\rE-mail: {fromEmail}\n\rSubject: {subject};\n\r \n\rMessage: {message}";
            mail.ReplyToList.Add(new MailAddress(fromEmail, name));

            await smtpClient.SendMailAsync(mail);
            return RedirectToAction("Index", "Home");
        }
    }
}