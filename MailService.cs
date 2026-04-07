using System.Net.Mail;

namespace ExposeBot;

public static class MailService
{
    public static async Task SendEmailAsync(string url)
    {
        SmtpClient smtpClient = new SmtpClient(EmailConfigs.HOST)
        {
            Port = EmailConfigs.PORT,
            Credentials = EmailConfigs.CREDENTIALS,
            EnableSsl = EmailConfigs.SSL,
        };

        MailMessage mailMessage = new MailMessage
        {
            From = EmailConfigs.FROM_ADDRESS,
            Subject = "Public URL from MailBot",
            Body = $"The public URL is: {url}. Expires at {DateTime.UtcNow.AddHours(5):yyyy-MM-dd HH:mm}",
            IsBodyHtml = EmailConfigs.IS_HTML,
        };
        mailMessage.To.Add("youremail@example.com");

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

}
