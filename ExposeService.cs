using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExposeBot;

public static class ExposeService
{
    public static Process ExposeProcess(string exposePath, string url) => new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = exposePath,
            Arguments = $"share {url}",
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        }
    };

    public static string ExtractPublicHttpUrl(string output)
    {
        var match = Regex.Match(output, @"Public HTTP:\s*(http[^\s]+)");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    public static string ExtractRemainingTime(string output)
    {
        var match = Regex.Match(output, @"Remaining time:\s*(\d{2}:\d{2}:\d{2})");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }
}
