using System;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines search method with setting options and before and after action callbacks
    /// </summary>
    public interface ISearchableEventable
    {
        /// <summary>
        /// The before search action callback. This method should be chained with <see cref="Search"/>
        /// </summary>
        Action OnBeforeSearch { get; set; }
        /// <summary>
        /// The search method. This mthod should be chained with <see cref="OnBeforeSearch"/>
        /// </summary>
        /// <returns></returns>
        Task Search();
        /// <summary>
        /// The after search action callback. This method should be called by <see cref="Search"/>
        /// </summary>
        Action OnAfterSearch { get; set; }
        /// <summary>
        /// Indicates when we should peform an initial search
        /// </summary>
        bool InitialAutoSearch { get; set; }
    }
}
