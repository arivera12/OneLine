using System;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Defines method to save with before and after action callbacks
    /// </summary>
    public interface ISaveableEventable
    {
        /// <summary>
        /// The save method. This method should be chained with <see cref="OnBeforeSave"/>
        /// </summary>
        /// <returns></returns>
        Task Save();
        /// <summary>
        /// The before save action callback. This method should be chained with <see cref="Save"/>
        /// </summary>
        Action OnBeforeSave { get; set; }
        /// <summary>
        /// The after saver action callback. This method should be called by <see cref="Save"/>
        /// </summary>
        Action OnAfterSave { get; set; }
    }
}
