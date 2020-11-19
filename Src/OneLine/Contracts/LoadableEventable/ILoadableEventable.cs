using System;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines a class that has loading and before and after loading capabilities
    /// </summary>
    public interface ILoadableEventable
    {
        /// <summary>
        /// The load method. This method should be chained with <see cref="OnAfterLoad"/>
        /// </summary>
        /// <returns></returns>
        Task Load();
        /// <summary>
        /// The on before load method. This method should be chained with <see cref="Load"/>
        /// </summary>
        Action OnBeforeLoad { get; set; }
        /// <summary>
        /// The on after load method.
        /// </summary>
        Action OnAfterLoad { get; set; }
        /// <summary>
        /// Let's know when auto load is required
        /// </summary>
        bool AutoLoad { get; set; }
    }
}
