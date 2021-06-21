using OneLine.Enums;

namespace OneLine.Models
{
    /// <summary>
    /// Defines model to save a form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISaveForm<T> : IDataHolder<T>
    {
        /// <summary>
        /// The save operation to perform to the form data.
        /// </summary>
        public SaveOperation SaveOperation { get; set; }
    }
}
