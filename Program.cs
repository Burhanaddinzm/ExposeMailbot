using ExposeBot;
using System.Text;

async Task StartMailBotService()
{
    Console.WriteLine("Starting MailBot service...");

    while (true)
    {
        var exposeProcess = ExposeService.ExposeProcess("/usr/bin/expose", "http://localhost:8096");
        bool shouldRestart = false;
        int lastProcessedIndex = 0;

        var output = new StringBuilder();
        exposeProcess.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null) output.AppendLine(e.Data);
        };

        exposeProcess.Start();
        exposeProcess.BeginOutputReadLine();

        Console.WriteLine("Process started. Monitoring output...");
        await Task.Delay(2000);

        string extractedUrl = ExposeService.ExtractPublicHttpUrl(output.ToString());
        if (!string.IsNullOrEmpty(extractedUrl))
        {
            Console.WriteLine($"Extracted URL: {extractedUrl}");
            await MailService.SendEmailAsync(extractedUrl);
        }
        else
        {
            Console.WriteLine("No 'Public HTTP' URL found in the output.");
            shouldRestart = true;
        }

        while (!shouldRestart)
        {
            await Task.Delay(1000);
            string currentOutput = output.ToString();

            string newOutput = currentOutput.Substring(lastProcessedIndex);
            lastProcessedIndex = currentOutput.Length;

            var newLines = newOutput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in newLines)
            {
                string remainingTime = ExposeService.ExtractRemainingTime(line);

                if (!string.IsNullOrEmpty(remainingTime))
                {
                    Console.WriteLine($"Time Remaining: {remainingTime}");

                    if (remainingTime == "00:00:01")
                    {
                        Console.WriteLine("Time almost up. Restarting process...");
                        shouldRestart = true;
                        Console.Clear();
                        break;
                    }
                }
            }
        }

        exposeProcess.Close();
        Console.WriteLine("Process closed. Restarting...");
    }
}

await StartMailBotService();
