namespace SiapBackups.Configuration.DateTime;

public readonly struct DateAndBaseHour
{
    public static readonly TimeZoneInfo BrazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public static readonly System.DateTime CurrentDateTime = TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, BrazilTimeZone);
}