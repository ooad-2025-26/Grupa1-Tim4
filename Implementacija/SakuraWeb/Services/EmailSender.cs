using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace SakuraWeb.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly AuthMessageSenderOptions _options;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        _options = optionsAccessor.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(_options.FromEmail))
        {
            throw new Exception("Null FromEmail");
        }
        else if (string.IsNullOrEmpty(_options.AppPassword))
        {
            throw new Exception("Null AppPassword");
        }

        await Execute(_options.FromEmail, _options.AppPassword, subject, htmlMessage, toEmail);
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage, string? fromEmail, string? appPassword)
    {
        var sendFrom = !string.IsNullOrEmpty(fromEmail) ? fromEmail : _options.FromEmail;
        var sendPassword = !string.IsNullOrEmpty(appPassword) ? appPassword : _options.AppPassword;

        if (string.IsNullOrEmpty(sendFrom))
        {
            throw new Exception("Null FromEmail");
        }
        else if (string.IsNullOrEmpty(sendPassword))
        {
            throw new Exception("Null AppPassword");
        }

        await Execute(sendFrom, sendPassword, subject, htmlMessage, toEmail);
    }

    public async Task Execute(string fromEmail, string appPassword, string subject, string htmlMessage, string toEmail)
    {
        using var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, appPassword),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception caught in SendEmailAsync(): {e}");
        }
    }
}