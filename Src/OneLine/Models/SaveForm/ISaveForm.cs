using OneLine.Enums;

namespace OneLine.Models
{
    /// <summary>
    /// Defines model to save a form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISaveForm<T>
    {
        /// <summary>
        /// The form data to save.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// The save operation to perform to the form data.
        /// </summary>
        public SaveOperation SaveOperation { get; set; }
    }
}
