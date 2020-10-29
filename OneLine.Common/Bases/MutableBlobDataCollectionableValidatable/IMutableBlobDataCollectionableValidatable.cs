using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of blob data representation
    /// </summary>
    public interface IMutableBlobDataCollectionableValidatable
    {
        /// <summary>
        /// Validates mutable blob datas
        /// </summary>
        /// <returns></returns>
        Task ValidateMutableBlobDatas();
        /// <summary>
        /// Clears mutable blob datas with rules
        /// </summary>
        void ClearMutableBlobDatasWithRules();
        /// <summary>
        /// Get mutable blob datas with rules properties of the model context
        /// </summary>
        /// <returns></returns>
        IEnumerable<PropertyInfo> GetMutableBlobDatasWithRulesProperties();
    }
}
