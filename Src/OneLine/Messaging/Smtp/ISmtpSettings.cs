namespace OneLine.Messaging
{
    /// <summary>
    /// Defines a smpt setting to be used in a Smpt client
    /// </summary>
    public interface ISmtpSettings
    {
        /// <summary>
        /// The email address
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// The display name
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// Use default credentials indicator
        /// </summary>
        bool UseDefaultCredentials { get; set; }
        /// <summary>
        /// The smtp host address
        /// </summary>
        string Host { get; set; }
        /// <summary>
        /// The host port
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// Enable Ssl indicator
        /// </summary>
        bool EnableSsl { get; set; }
        /// <summary>
        /// The timeout for the smtp client
        /// </summary>
        int Timeout { get; set; }
        /// <summary>
        /// The username for the smtp
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The password for the smtp
        /// </summary>
        string Password { get; set; }
    }
}
