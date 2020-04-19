namespace OneLine.Messaging
{
    public class SendGridApiSettings : ISendGridApiSettings
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
