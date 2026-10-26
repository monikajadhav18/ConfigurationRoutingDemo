namespace ConfigurationRoutingDemo.Models;

public class AppSettings
{
    public string ApplicationName { get; set; } = string.Empty;

    public int MaxEmployees { get; set; }

    public bool EnableReports { get; set; }
}