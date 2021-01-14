using OneLine.Enums;

namespace OneLine.Contracts
{
    /// <summary>
    /// The collection mode wether is appendable or replaceable mode
    /// </summary>
    public interface ICollectionAppendableReplaceableModeable
    {
        /// <summary>
        /// The collection mode appendable or replaceable
        /// </summary>
        CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
    }
}
