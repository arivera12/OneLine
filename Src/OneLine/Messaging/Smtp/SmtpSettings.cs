namespace OneLine.Messaging
{
    public class SmtpSettings : ISmtpSettings
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool UseDefaultCredentials { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string Host { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Port { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool EnableSsl { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Timeout { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string Password { get; set; }
    }
}

