using Microsoft.AspNetCore.Components;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Blazor.Components
{
    public class UserBlobsManagerBase : ComponentBase, IComponent, IUserBlobsManager
    {
        public IEnumerable<UserBlobs> UserBlobs { get; set; }
        public EventCallback<IEnumerable<UserBlobs>> UserBlobsChanged { get; set; }
        public bool Actionable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool Deletable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool Downloadable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool PreViewable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public EventCallback<UserBlobs> OnClick { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool Hidden { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Delete(UserBlobs userBlobs)
        {
            throw new System.NotImplementedException();
        }

        public Task Download(UserBlobs userBlobs)
        {
            throw new System.NotImplementedException();
        }

        public Task PreView(UserBlobs userBlobs)
        {
            throw new System.NotImplementedException();
        }
    }
}
