using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace RentalCars.Infra.Email.Services;

public sealed class EmailService : IEmailService
{

    #region Properties

    private readonly string _emailSender;
    private readonly string _passwordSender;
    private readonly string _server;
    private readonly int _port;

    #endregion

    #region Constructor

    public EmailService(IConfiguration configuration)
    {
        _emailSender = configuration.GetValue<string>("EmailSettings:emailSender");
        _passwordSender = configuration.GetValue<string>("EmailSettings:passwordEmailTest");
        _server = configuration.GetValue<string>("EmailSettings:server");
        _port = configuration.GetValue<int>("EmailSettings:port");
    }

    #endregion

    public void SendEmail(string to, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSender),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            using (var smtpClient = new SmtpClient(_server, _port))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSender, _passwordSender);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }
    }

}
