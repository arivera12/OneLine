namespace OneLine.Messaging
{
    public class SendGridApiSettings : ISendGridApiSettings
    {
        /// <inheritdoc/>
        public string SendGridUser { get; set; }
        /// <inheritdoc/>
        public string SendGridKey { get; set; }
        /// <inheritdoc/>
        public string Email { get; set; }
        /// <inheritdoc/>
        public string DisplayName { get; set; }
    }
}
