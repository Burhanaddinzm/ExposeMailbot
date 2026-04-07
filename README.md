# ExposeBot

ExposeBot is a .NET 8.0 application designed to automate the process of exposing a local service to the internet using the `expose` CLI tool and sending the generated public URL via email. It continuously monitors the output of the `expose` process, extracts the public URL, and sends it via email. The bot also monitors the remaining time of the exposed service and restarts the process when the time is almost up.

## Features
- Automatically starts and monitors the `expose` CLI tool.
- Extracts the public HTTP URL from the `expose` output.
- Sends the public URL via email using SMTP.
- Monitors the remaining time of the exposed service.
- Restarts the process when the time is almost up.

## Prerequisites
- .NET 8.0 SDK
- `expose` CLI tool installed and available in the system PATH.
- SMTP credentials for sending emails.

## Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd ExposeBot
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

## Configuration
Configure the email settings in the `ExposeBot/Configs.cs` file:

```csharp
public static class EmailConfigs
{
    public static string HOST => "live.smtp.mailtrap.io";
    public static int PORT => 587;
    public static NetworkCredential CREDENTIALS => new NetworkCredential("api", "YOUR_API_KEY");
    public static bool SSL => true;
    public static MailAddress FROM_ADDRESS => new MailAddress("hello@demomailtrap.com");
    public static bool IS_HTML => false;
}
```

Replace `YOUR_API_KEY` with your actual SMTP API key and update the `FROM_ADDRESS` and `HOST` as needed.

## Usage
1. Run the application:
   ```bash
   dotnet run --project ExposeBot
   ```

2. The application will start the `expose` process and monitor its output. Once the public URL is extracted, it will be sent via email.

3. The application will continue to monitor the remaining time of the exposed service and restart the process when the time is almost up.

## Project Structure
- `ExposeBot/`: Contains the main project files.
  - `Program.cs`: Entry point of the application.
  - `ExposeService.cs`: Handles the `expose` process and extracts relevant information from its output.
  - `MailService.cs`: Manages sending emails via SMTP.
  - `Configs.cs`: Contains configuration settings for the email service.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
