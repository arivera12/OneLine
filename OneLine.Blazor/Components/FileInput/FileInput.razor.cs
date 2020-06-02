using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDownloadFile;
using System.Net.Http;
using CurrieTechnologies.Razor.SweetAlert2;
using OneLine.Blazor.Extensions;
using OneLine.Bases;
using OneLine.Validations;
using OneLine.Models.Users;

namespace OneLine.Blazor.Components
{
    public class FileInputBase : ComponentBase, IComponent
    {
        /// <summary>
        /// Input File extra attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        /// <summary>
        /// Allows to attach and upload multiple file uploads.
        /// </summary>
        [Parameter] public bool IsMultiple { get; set; }
        /// <summary>
        /// Class to apply on drag enter. Default: Default drop-zone-drop-target-drag.
        /// </summary>
        [Parameter] public string DropTargetDragClass { get; set; }
        /// <summary>
        /// Class to apply on drag leave. Default: drop-zone-drop-target.
        /// </summary>
        [Parameter] public string DropTargetClass { get; set; }
        /// <summary>
        /// Readed files from the device file system.
        /// </summary>
        [Parameter] public IEnumerable<BlobData> BlobDatas { get; set; }
        /// <summary>
        /// Callback to return the readed files from the device file system.
        /// </summary>
        [Parameter] public EventCallback<IEnumerable<BlobData>> BlobDatasChanged { get; set; }
        /// <summary>
        /// Userblobs attached to the current property
        /// </summary>
        [Parameter] public IEnumerable<UserBlobs> UserBlobs { get; set; }
        /// <summary>
        /// Callback to return the userblobs
        /// </summary>
        [Parameter] public EventCallback<IEnumerable<UserBlobs>> UserBlobsChanged { get; set; }
        /// <summary>
        /// The buffer size to read the files.
        /// </summary>
        [Parameter] public int BufferSize { get; set; }
        /// <summary>
        /// The input name is the property name of the model associate with the file input. 
        /// </summary>
        [Parameter] public string InputName { get; set; }
        /// <summary>
        /// The border color on highlight. Default orangered. This property is ignored if DropTargetDragClass is changed of his default drop-zone-drop-target-drag.
        /// </summary>
        [Parameter] public string BorderColor { get; set; }
        /// <summary>
        /// Hides the entire component
        /// </summary>
        [Parameter] public bool Hidden { get; set; }
        /// <summary>
        /// Hide Clear button
        /// </summary>
        [Parameter] public bool HideResetButton { get; set; }
        /// <summary>
        /// Reset button text. Default Reset
        /// </summary>
        [Parameter] public MarkupString ResetButtonText { get; set; }
        /// <summary>
        /// Hide add button
        /// </summary>
        [Parameter] public bool HideAddButton { get; set; }
        /// <summary>
        /// Add button text. Default Add
        /// </summary>
        [Parameter] public MarkupString AddButtonText { get; set; }
        /// <summary>
        /// Hide the dropzone
        /// </summary>
        [Parameter] public bool HideDropZone { get; set; }
        /// <summary>
        /// The dropzone label text. Default DropFilesHere
        /// </summary>
        [Parameter] public MarkupString DropZoneText { get; set; }
        /// <summary>
        /// The dropzone inline style.
        /// Default:
        /// <style>
        ///     div.drop-zone-drop-target {
        ///        display: block;
        ///        padding: 20px;
        ///        margin-bottom: 10px;
        ///        border: 1px dashed black;
        ///        border-radius: 5px;
        ///        position: relative;
        ///     }
        ///     div.drop-zone-drop-target-drag {
        ///        border-color: @BorderColor;
        ///        font-weight: bold;
        ///     }
        ///</style>
        /// </summary>
        [Parameter] public MarkupString DropZoneInlineStyle { get; set; }
        /// <summary>
        /// Prevents download link to download the current clicked file
        /// </summary>
        [Parameter] public bool PreventDownload { get; set; }
        /// <summary>
        /// Hides the delete button
        /// </summary>
        [Parameter] public bool HideDeleteButton { get; set; }
        [Inject] public IFileReaderService FileReaderService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        public HttpBaseUserBlobsService<UserBlobs, Identifier<string>, string, BlobData, BlobDataValidator> HttpBaseUserBlobsService { get; set; }
        public ElementReference DropTarget { get; set; }
        public ElementReference DropTargetInput { get; set; }
        public IFileReaderRef DropReference { get; set; }
        public IFileReaderRef DropInputReference { get; set; }
        public string DropClass { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HttpBaseUserBlobsService = new HttpBaseUserBlobsService<UserBlobs, Identifier<string>, string, BlobData, BlobDataValidator>()
                {
                    HttpClient = HttpClient
                };
                if (string.IsNullOrWhiteSpace(DropTargetDragClass))
                {
                    DropTargetDragClass = "drop-zone-drop-target-drag";
                }
                if (string.IsNullOrWhiteSpace(DropTargetClass))
                {
                    DropTargetClass = "drop-zone-drop-target";
                }
                if (string.IsNullOrWhiteSpace(DropZoneText.Value))
                {
                    DropZoneText = (MarkupString)Resourcer.GetString("DropFilesHere");
                }
                if (string.IsNullOrWhiteSpace(ResetButtonText.Value))
                {
                    ResetButtonText = (MarkupString)Resourcer.GetString("Reset");
                }
                if (string.IsNullOrWhiteSpace(AddButtonText.Value))
                {
                    AddButtonText = (MarkupString)Resourcer.GetString("Add");
                }
                BorderColor = string.IsNullOrWhiteSpace(BorderColor) ? "orangered" : BorderColor;
                if (string.IsNullOrWhiteSpace(DropZoneInlineStyle.Value))
                {
                    DropZoneInlineStyle = (MarkupString)$@"<style>
    div.drop-zone-drop-target {{
        display: block;
        padding: 20px;
        margin-bottom: 10px;
        border: 1px dashed black;
        border-radius: 5px;
        position: relative;
    }}
    div.drop-zone-drop-target-drag {{
        border-color: {BorderColor};
        font-weight: bold;
    }}
</style>";
                }
                DropClass = DropTargetClass;
                DropInputReference = FileReaderService.CreateReference(DropTargetInput);
                if(!HideDropZone)
                {
                    DropReference = FileReaderService.CreateReference(DropTarget);
                    await DropReference.RegisterDropEventsAsync(IsMultiple);
                }
                StateHasChanged();
            }
        }
        public async Task Clear()
        {
            await DropReference.ClearValue();
            await DropInputReference.ClearValue();
            var blobDatas = new List<BlobData>();
            BlobDatas = blobDatas;
            await BlobDatasChanged.InvokeAsync(blobDatas);
            StateHasChanged();
        }
        public async Task OpenDeviceFileSystem()
        {
            await JSRuntime.InvokeVoidAsync("eval", $"document.querySelector('[_bl_{DropTargetInput.Id}]').click()");
        }
        public async Task Remove(BlobData blobData)
        {
            if (!HideDeleteButton && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToDeleteTheFile"),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                var blobDatas = new List<BlobData>(BlobDatas);
                blobDatas.Remove(blobData);
                BlobDatas = blobDatas;
                await BlobDatasChanged.InvokeAsync(BlobDatas);
            }
            StateHasChanged();
        }
        public async Task Download(BlobData blobData)
        {
            if(!PreventDownload && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToDownloadTheFile"),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                await BlazorDownloadFileService.DownloadFile(blobData.Name, blobData.Data);
            }
        }
        public async Task Remove(UserBlobs userBlob)
        {
            if (!HideDeleteButton && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToDeleteTheFile"),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                var userBlobs = new List<UserBlobs>(UserBlobs);
                userBlobs.Remove(userBlob);
                UserBlobs = userBlobs;
                await BlobDatasChanged.InvokeAsync(BlobDatas);
            }
            StateHasChanged();
        }
        public async Task Download(UserBlobs userBlobs)
        {
            if (!PreventDownload && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToDownloadTheFile"),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                var responseResult = await HttpBaseUserBlobsService.DownloadBinary(new Identifier<string>(userBlobs.UserBlobId), new BlobDataValidator());
                if (!responseResult.Succeed && !responseResult.HasException)
                {
                    await SweetAlertService.FireAsync(Resourcer.GetString("SessionExpired"), Resourcer.GetString("YourSessionHasExpiredPleaseLoginInBackAgain"), SweetAlertIcon.Warning);
                    await ApplicationState<AspNetUsersViewModel>.LogoutAndNavigateTo("/login");
                }
                else if (responseResult.Succeed)
                {
                    await BlazorDownloadFileService.DownloadFile(userBlobs.FileName, responseResult.Response);
                }
                else if (responseResult.HasException)
                {
                    await SweetAlertService.FireAsync(null, responseResult.Exception.Message, SweetAlertIcon.Error);
                }
                else
                {
                    await SweetAlertService.FireAsync(null, Resourcer.GetString(responseResult.Response.ToString()), SweetAlertIcon.Error);
                }
                StateHasChanged();
            }
        }
        public void OnDragEnter(EventArgs e)
        {
            DropClass = $"{DropTargetClass} {DropTargetDragClass}";
            StateHasChanged();
        }
        public async Task OnDrop(EventArgs e)
        {
            await ReadFiles();
        }
        public void OnDragLeave(EventArgs e)
        {
            DropClass = DropTargetClass;
            StateHasChanged();
        }
        public async Task OnInputChange(EventArgs e)
        {
            await ReadFiles();
        }
        public async Task ReadFiles()
        {
            DropClass = DropTargetClass;
            StateHasChanged();
            var blobDatas = new ObservableRangeCollection<BlobData>();
            if(IsMultiple)
            {
                IEnumerable<IFileReference> fileReferences = null;
                if (!HideDropZone)
                {
                    fileReferences = await DropReference.EnumerateFilesAsync();
                    fileReferences = fileReferences.Concat(await DropInputReference.EnumerateFilesAsync());
                }
                else
                {
                    fileReferences = await DropInputReference.EnumerateFilesAsync();
                }
                foreach (var fileReference in fileReferences)
                {
                    var fileInfo = await fileReference.ReadFileInfoAsync();
                    var blobData = new BlobData
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Size,
                        Type = fileInfo.Type,
                        InputName = InputName,
                        LastModified = fileInfo.LastModifiedDate ?? default
                    };
                    var stream = BufferSize <= 0 ? await fileReference.CreateMemoryStreamAsync() : await fileReference.CreateMemoryStreamAsync(BufferSize);
                    blobData.Data = stream;
                    blobDatas.Add(blobData);
                }
            }
            else
            {
                IEnumerable<IFileReference> fileReferences = null;
                if (!HideDropZone)
                {
                    fileReferences = await DropReference.EnumerateFilesAsync();
                }
                if(fileReferences.IsNullOrEmpty())
                {
                    fileReferences = await DropInputReference.EnumerateFilesAsync();
                }
                foreach (var fileReference in fileReferences)
                {
                    var fileInfo = await fileReference.ReadFileInfoAsync();
                    var blobData = new BlobData
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Size,
                        Type = fileInfo.Type,
                        InputName = InputName,
                        LastModified = fileInfo.LastModifiedDate ?? default
                    };
                    var stream = BufferSize <= 0 ? await fileReference.CreateMemoryStreamAsync() : await fileReference.CreateMemoryStreamAsync(BufferSize);
                    blobData.Data = stream;
                    blobDatas.Add(blobData);
                    break;
                }
            }
            if (BlobDatas.IsNullOrEmpty() || !IsMultiple)
            {
                BlobDatas = blobDatas;
            }
            else
            {
                foreach (var blobData in BlobDatas.ToList())
                {
                    if (blobDatas.Any(w =>  w.Name == blobData.Name && 
                                            w.Size == blobData.Size && 
                                            w.Type == blobData.Type))
                    {
                        blobDatas.Remove(blobData);
                    }
                    else
                    {
                        blobDatas.Add(blobData);
                    }
                }
                BlobDatas = blobDatas;
            }
            await BlobDatasChanged.InvokeAsync(BlobDatas);
            if (!HideDropZone)
            {
                await DropReference.ClearValue();
            }
            await DropInputReference.ClearValue();
        }
    }
}
