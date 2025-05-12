using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Services.Abstraction;

namespace Services;
public class EmailService(IConfiguration _configuration) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = body
        };

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]!), SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
