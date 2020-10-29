namespace OneLine.Messaging
{
    /// <summary>
    /// The send grid api settings
    /// </summary>
    public interface ISendGridApiSettings
    {
        /// <summary>
        /// The send grid user
        /// </summary>
        string SendGridUser { get; set; }
        /// <summary>
        /// The send grid key
        /// </summary>
        string SendGridKey { get; set; }
        /// <summary>
        /// The send grid email address
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// The send grid display name
        /// </summary>
        string DisplayName { get; set; }
    }
}
