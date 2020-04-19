namespace OneLine.Messaging
{
    public interface ISmtpSettings
    {
        string Email { get; set; }
        string DisplayName { get; set; }
        bool UseDefaultCredentials { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        bool EnableSsl { get; set; }
        int Timeout { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
