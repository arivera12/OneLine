namespace OneLine.Models
{
    /// <summary>
    /// Soft Deletable interface
    /// </summary>
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}

