using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of blob data representation
    /// </summary>
    /// <typeparam name="TBlobData">The blob data type</typeparam>
    public interface IBlobDataCollectionableValidatable
    {
        Task ValidateBlobDatas();
        bool HasBlobDatasWithRules();
        void ClearBlobDatasWithRules();
        IEnumerable<PropertyInfo> GetBlobDatasWithRulesProperties();
    }
}
