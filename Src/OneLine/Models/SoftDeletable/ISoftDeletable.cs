namespace OneLine.Models
{
    /// <summary>
    /// Defines a soft deletable structure
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Is deleted indicator property
        /// </summary>
        bool? IsDeleted { get; set; }
    }
}

