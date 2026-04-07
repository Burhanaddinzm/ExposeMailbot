using System.Net;
using System.Net.Mail;

namespace ExposeBot;

public static class EmailConfigs
{
    public static string HOST => "live.smtp.mailtrap.io";
    public static int PORT => 587;
    public static NetworkCredential CREDENTIALS => new NetworkCredential("api", "YOUR_API_KEY");
    public static bool SSL => true;
    public static MailAddress FROM_ADDRESS => new MailAddress("hello@demomailtrap.com");
    public static bool IS_HTML => false;
}
