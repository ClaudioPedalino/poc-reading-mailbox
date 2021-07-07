using AE.Net.Mail;

public static class Config
{
    public static readonly string Host = "imap.gmail.com";
    public static string Username = string.Empty;
    public static string Password = string.Empty;
    public static readonly AuthMethods Method = AuthMethods.Login;
    public static readonly int Port = 993;
    public static readonly bool Secure = true;
}