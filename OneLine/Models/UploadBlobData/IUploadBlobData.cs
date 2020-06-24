using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IUploadBlobData
    {
        /// <summary>
        /// The blob datas to upload
        /// </summary>
        IEnumerable<BlobData> BlobDatas { get; set; }
        /// <summary>
        /// The form file rules.
        /// </summary>
        FormFileRules FormFileRules { get; set; }
        /// <summary>
        /// The property name should be the same as the file input name. 
        /// This field must and should match the property name where blob reference will be stored.
        /// This reference property must be always a byte[] data type to work properly.
        /// </summary>
        string PropertyName { get; set; }
        /// <summary>
        /// Forces a file upload on update operation. Overrides IsRequired property rule.
        /// </summary>
        bool ForceUpload { get; set; }
    }
}
