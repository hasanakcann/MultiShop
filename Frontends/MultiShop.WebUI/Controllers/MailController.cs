using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Controllers;

public class MailController : Controller
{
    [HttpGet]
    public IActionResult SendMail()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendMail(MailRequest mailRequest)
    {
        MimeMessage emailMessage = new MimeMessage();

        MailboxAddress senderAddress = new MailboxAddress("MultiShop", "multishop@gmail.com");
        emailMessage.From.Add(senderAddress);

        MailboxAddress recipientAddress = new MailboxAddress("User", mailRequest.ReceiverMail);
        emailMessage.To.Add(recipientAddress);

        var messageBodyBuilder = new BodyBuilder
        {
            TextBody = mailRequest.MailContent
        };
        emailMessage.Body = messageBodyBuilder.ToMessageBody();
        emailMessage.Subject = mailRequest.Subject;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtpClient.Authenticate("multishop@gmail.com", "application-password");
        smtpClient.Send(emailMessage);
        smtpClient.Disconnect(true);

        return View();
    }
}
