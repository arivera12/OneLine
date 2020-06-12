using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IUploadBlobData
    {
        /// <summary>
        /// The blob datas to upload
        /// </summary>
        IEnumerable<IBlobData> BlobDatas { get; set; }
        /// <summary>
        /// The form file rules.
        /// </summary>
        IFormFileRules FormFileRules { get; set; }
        /// <summary>
        /// The property name should be the same as the file input name. 
        /// This field must and should match the property name where blob reference will be stored.
        /// This reference property must be always a byte[] data type to work properly.
        /// </summary>
        string PropertyName { get; set; }
        /// <summary>
        /// The property that holds the uploaded data. 
        /// This field must and should match the property name that holds the uploaded data.
        /// This reference property must be always a IEnumerable<IBlobData> or IEnumerable<BlobData> data type to work properly. 
        /// </summary>
        string PropertyNameBlobData { get; set; }
        /// <summary>
        /// Forces a file upload on update operation.
        /// </summary>
        bool ForceUploadOnUpdate { get; set; }
    }
}
