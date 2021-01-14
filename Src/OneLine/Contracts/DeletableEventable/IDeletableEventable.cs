using System;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    /// <summary>
    /// The class is Deletable and eventable
    /// </summary>
    public interface IDeletableEventable
    {
        /// <summary>
        /// The delete task
        /// </summary>
        /// <returns></returns>
        Task Delete();
        /// <summary>
        /// The action before delete
        /// </summary>
        Action OnBeforeDelete { get; set; }
        /// <summary>
        /// The action after delete
        /// </summary>
        Action OnAfterDelete { get; set; }
    }
}
