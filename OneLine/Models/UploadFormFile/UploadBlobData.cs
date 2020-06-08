using System.Collections.Generic;

namespace OneLine.Models
{
    public class UploadBlobData : IUploadBlobData 
    {
        /// <summary>
        /// The blob datas to upload
        /// </summary>
        public virtual IEnumerable<IBlobData> BlobDatas { get; set; }
        /// <summary>
        /// The form file rules.
        /// </summary>
        public virtual IFormFileRules FormFileRules { get; set; }
        /// <summary>
        /// Forces a file upload on update operation.
        /// </summary>
        public virtual bool ForceUploadOnUpdate { get; set; }
        public UploadBlobData()
        { }
        public UploadBlobData(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules)
        {
            BlobDatas = blobDatas;
            FormFileRules = formFileRules;
        }
        public UploadBlobData(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules, bool forceUploadOnUpdate)
        {
            BlobDatas = blobDatas;
            FormFileRules = formFileRules;
            ForceUploadOnUpdate = forceUploadOnUpdate;
        }
    }
}