using System.Collections.Generic;

namespace OneLine.Bases
{
    public interface IBlobData<TBlobData>
    {
        IList<TBlobData> BlobDatas { get; set; }
    }
}
