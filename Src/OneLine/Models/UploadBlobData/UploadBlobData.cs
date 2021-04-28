using System.Collections.Generic;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure to manage files on a integrated way on the client and server side
    /// </summary>
    public class UploadBlobData : IUploadBlobData 
    {
        /// <inheritdoc/>
        public virtual IEnumerable<IBlobData> BlobDatas { get; set; }
        /// <inheritdoc/>
        public virtual IFormFileRules FormFileRules { get; set; }
        /// <inheritdoc/>
        public virtual string PropertyName { get; set; }
        /// <inheritdoc/>
        public virtual bool ForceUpload { get; set; }
        /// <summary>
        /// The default constructor
        /// </summary>
        public UploadBlobData()
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        public UploadBlobData(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules)
        {
            BlobDatas = blobDatas;
            FormFileRules = formFileRules;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <param name="propertyName"></param>
        public UploadBlobData(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules, string propertyName)
        {
            BlobDatas = blobDatas;
            FormFileRules = formFileRules;
            PropertyName = propertyName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <param name="propertyName"></param>
        /// <param name="forceUpload"></param>
        public UploadBlobData(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules, string propertyName, bool forceUpload)
        {
            BlobDatas = blobDatas;
            FormFileRules = formFileRules;
            PropertyName = propertyName;
            ForceUpload = forceUpload;
        }
    }
}