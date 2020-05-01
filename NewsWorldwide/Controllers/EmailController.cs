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
            //This is configed to use gmail as domain so for gredentials
            //should be entered a mail.gmail account
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            //For NetworkCredentials enter the account credentials
            /*
             * NetworkCredentials(string userName, string password)
             * userName = example@gmail.com
             * password = yourpassword
             */
            smtpClient.Credentials = new System.Net.NetworkCredential("example@gmail.com", "yourpassword");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress(form.FromEmail, "News Web Site");
            mail.To.Add(new MailAddress("The mail ON which you want to receive all messages"));
            mail.Subject = $"News Web Site Contact on: \"{form.Subject}\"";
            mail.Body = $"From: {form.Name};\n\rE-mail: {form.FromEmail}\n\rSubject: {form.Subject};\n\r \n\rMessage: {form.Message}";
            mail.ReplyToList.Add(new MailAddress(form.FromEmail, form.Name));

            await smtpClient.SendMailAsync(mail);
            return RedirectToAction("Index", "Home");
        }
    }
}