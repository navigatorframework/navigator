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

    /// <summary>
    ///     Creates a structured data block for metrics or detailed information.
    /// </summary>
    /// <param name="data">Dictionary of key-value pairs to display.</param>
    /// <returns>Formatted data block.</returns>
    public static string DataBlock(Dictionary<string, string> data)
    {
        var result = new StringBuilder();
        result.AppendLine("```");
        
        foreach (var (key, value) in data)
        {
            result.AppendLine($"{key}: {value}");
        }
        
        result.AppendLine("```");
        return result.ToString();
    }

    /// <summary>
    ///     Creates a simple list block for displaying multiple items.
    /// </summary>
    /// <param name="items">List of items to display.</param>
    /// <param name="title">Optional title for the list.</param>
    /// <returns>Formatted list block.</returns>
    public static string ListBlock(IEnumerable<string> items, string? title = null)
    {
        var result = new StringBuilder();
        
        if (!string.IsNullOrEmpty(title))
        {
            result.AppendLine($"<b>{title}</b>");
            result.AppendLine();
        }

        foreach (var item in items)
        {
            result.AppendLine($"• {item}");
        }
        
        return result.ToString();
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
