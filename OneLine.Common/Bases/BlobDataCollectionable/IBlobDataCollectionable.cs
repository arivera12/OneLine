using System;
using System.Collections.Generic;

namespace OneLine.Bases
{
    /// <summary>
    /// This interface is a definition of blob data representation
    /// </summary>
    /// <typeparam name="TBlobData">The blob data type</typeparam>
    public interface IBlobDataCollectionable<TBlobData>
    {
        IList<TBlobData> BlobDatas { get; set; }
        Action<IList<TBlobData>> BlobDatasChanged { get; set; }
    }
}
