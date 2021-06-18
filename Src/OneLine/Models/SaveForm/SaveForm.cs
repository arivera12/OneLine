using OneLine.Enums;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a model to save a form.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SaveForm<T> : ISaveForm<T>
    {
        /// <inheritdoc/>
        public T Data { get; set; }
        /// <inheritdoc/>
        public SaveOperation SaveOperation { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public SaveForm()
        {
        }
        /// <summary>
        /// Constructor with the form data
        /// </summary>
        /// <param name="data"></param>
        public SaveForm(T data)
        {
            Data = data;
        }
        /// <summary>
        /// Constructor with the form data and the save operation
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveOperation"></param>
        public SaveForm(T data, SaveOperation saveOperation)
        {
            Data = data;
            SaveOperation = saveOperation;
        }
    }
}