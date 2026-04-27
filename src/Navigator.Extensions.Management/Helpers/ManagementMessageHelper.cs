using System.Text;

namespace Navigator.Extensions.Management.Helpers;

internal static class ManagementMessageHelper
{
    public static string Error(string action, string summary, string? details = null)
    {
        return FormatMessage("❌", "ERROR", action, summary, details);
    }

    public static string Success(string action, string summary, string? details = null)
    {
        return FormatMessage("✅", "SUCCESS", action, summary, details);
    }
    
    public static string Info(string action, string summary, string? details = null)
    {
        return FormatMessage("🔹", "INFO", action, summary, details);
    }

    public static string Warning(string action, string summary, string? details = null)
    {
        return FormatMessage("⚠️", "WARNING", action, summary, details);
    }
    
    public static string Debug(string action, string summary, string? details = null)
    {
        return FormatMessage("🔍", "DEBUG", action, summary, details);
    }

    private static string FormatMessage(string icon, string category, string action, string summary, string? details)
    {
        var result = new StringBuilder();
        
        result.AppendLine($"{icon} <b>{action}</b>");
        result.AppendLine();
        result.AppendLine(summary);
        
        if (!string.IsNullOrEmpty(details))
        {
            result.AppendLine();
            result.AppendLine(details);
        }
        
        return result.ToString();
    }
}
