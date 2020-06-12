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
        /// The property name should be the same as the file input name. 
        /// This field must and should match the property name where blob reference will be stored.
        /// This reference property must be always a byte[] data type to work properly.
        /// </summary>
        public virtual string PropertyName { get; set; }
        /// <summary>
        /// The property that holds the uploaded data. 
        /// This field must and should match the property name that holds the uploaded data.
        /// This reference property must be always a IEnumerable<IBlobData> or IEnumerable<BlobData> data type to work properly. 
        /// </summary>
        public virtual string PropertyNameBlobData { get; set; }
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