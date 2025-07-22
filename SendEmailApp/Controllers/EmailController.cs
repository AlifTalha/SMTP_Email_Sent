using Microsoft.AspNetCore.Mvc;
using SendEmailApp.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace SendEmailApp.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create the email message
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Test_Mail", "officework144923@gmail.com"));
                    message.To.Add(new MailboxAddress("Receiver", model.ToEmail));
                    message.Subject = model.Subject;

                    // Create the body builder
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = model.MessageBody
                    };

                    //  Optional file attachment
                    if (model.Attachment != null && model.Attachment.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.Attachment.CopyToAsync(memoryStream);
                            memoryStream.Position = 0;

                            bodyBuilder.Attachments.Add(model.Attachment.FileName, memoryStream.ToArray(), ContentType.Parse(model.Attachment.ContentType));
                        }
                    }

                    message.Body = bodyBuilder.ToMessageBody();

                    // Send the email via SMTP
                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync("officework144923@gmail.com", ""); // App password
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    ModelState.Clear(); 
                    ViewBag.Status = "Email sent successfully!";
                    return View(new EmailModel()); 
                }
                catch (Exception ex)
                {
                    ViewBag.Status = $" Error: {ex.Message}";
                }
            }

            return View(model); 
        }
    }
}
