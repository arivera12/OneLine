namespace OneLine.Messaging
{
    public class SmtpSettings : ISmtpSettings
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool UseDefaultCredentials { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public SmtpSettings()
        {

        }
    }
}

