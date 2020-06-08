using Microsoft.AspNetCore.Http;
using System;
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
        /// Forces a file upload on update operation.
        /// </summary>
        bool ForceUploadOnUpdate { get; set; }
    }
}
