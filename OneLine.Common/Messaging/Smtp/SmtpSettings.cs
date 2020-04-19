namespace OneLine.Messaging
{
    public class SmtpSettings : ISmtpSettings
    {
        public virtual string Email { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool UseDefaultCredentials { get; set; }
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
        public virtual bool EnableSsl { get; set; }
        public virtual int Timeout { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
    }
}

