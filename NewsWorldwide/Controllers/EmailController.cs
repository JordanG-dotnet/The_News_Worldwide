using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Models;

namespace NewsWorldwide.Controllers
{
    public class EmailController : Controller
    {
        public async Task<IActionResult> SendEmail(EmailFormViewModel form)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("dancho.development@gmail.com", "DanchoKapitancho1234");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("dancho.development@gmail.com", "News Web Site");
            mail.To.Add(new MailAddress("jordang.varna@gmail.com"));
            mail.Subject = $"News Web Site Contact on: \"{form.Subject}\"";
            mail.Body = $"From: {form.Name};\n\rE-mail: {form.FromEmail}\n\rSubject: {form.Subject};\n\r \n\rMessage: {form.Message}";
            mail.ReplyToList.Add(new MailAddress(form.FromEmail, form.Name));

            await smtpClient.SendMailAsync(mail);
            return RedirectToAction("Index", "Home");
        }
    }
}