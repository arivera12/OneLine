using Microsoft.AspNetCore.Components;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Blazor.Components
{
    public interface IUserBlobsManager
    {
        IEnumerable<UserBlobs> UserBlobs { get; set; }
        EventCallback<IEnumerable<UserBlobs>> UserBlobsChanged { get; set; }
        public bool Actionable { get; set; }
        public bool Deletable { get; set; }
        public bool Downloadable { get; set; }
        public bool PreViewable { get; set; }
        public bool Hidden { get; set; }
        void Delete(UserBlobs userBlobs);
        Task Download(UserBlobs userBlobs);
        Task PreView(UserBlobs userBlobs);
        EventCallback<UserBlobs> OnClick { get; set; }
    }
}
