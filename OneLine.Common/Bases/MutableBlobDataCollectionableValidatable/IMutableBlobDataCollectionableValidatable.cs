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
        Task ValidateMutableBlobDatas();
        void ClearMutableBlobDatasWithRules();
        IEnumerable<PropertyInfo> GetMutableBlobDatasWithRulesProperties();
    }
}
