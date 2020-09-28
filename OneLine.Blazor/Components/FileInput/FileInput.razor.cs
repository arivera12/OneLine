using Tewr.Blazor.FileReader;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using JsonLanguageLocalizerNet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Blazor.Extensions;
using OneLine.Blazor.Services;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
        [Parameter] public bool AllowMultiple { get; set; }
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
        /// The api version of the http client. 
        /// </summary>
        [Parameter] public string Api { get; set; }
        /// <summary>
        /// The controller name of the http client. 
        /// </summary>
        [Parameter] public string ControllerName { get; set; }
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
        /// Prevents adding files
        /// </summary>
        [Parameter] public bool PreventAdding { get; set; }
        /// <summary>
        /// Add button text. Default Add
        /// </summary>
        [Parameter] public MarkupString AddButtonText { get; set; }
        /// <summary>
        /// The dropzone label text. Default DropFilesHere or DropFilesOrClickHere
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
        /// <summary>
        /// The minimum allowed file to attach to the file input
        /// </summary>
        [Parameter] public int MinimumAllowedFiles { get; set; }
        /// <summary>
        /// The maximun allowed file to attach to the file input
        /// </summary>
        [Parameter] public int MaximumAllowedFiles { get; set; }
        /// <summary>
        /// The max file size measured in bytes
        /// </summary>
        [Parameter] public long MaxFileSize { get; set; }
        /// <summary>
        /// The max file size measured text. This property must be used in conjuction with MaxFileSize to display in preference measured like b, kb, mb, gb, etc.
        /// </summary>
        [Parameter] public string MaxFileSizeMeasuredText { get; set; }
        /// <summary>
        /// Property to be used to let know if the minimum allowed files is reached
        /// </summary>
        [Parameter] public bool MinimumAllowedFilesReached { get; set; }
        /// <summary>
        /// Property to be used to let know if the maximum allowed files is reached
        /// </summary>
        [Parameter] public bool MaximumAllowedFilesReached { get; set; }
        /// <summary>
        /// Event callback to be used to let know if the minimum allowed files is reached
        /// </summary>
        [Parameter] public EventCallback<bool> MinimumAllowedFilesReachedChanged { get; set; }
        /// <summary>
        /// Event callback to be used to let know if the maximum allowed files is reached
        /// </summary>
        [Parameter] public EventCallback<bool> MaximumAllowedFilesReachedChanged { get; set; }
        /// <summary>
        /// Hides the text below the dropzone containing the info about maximum size per file, mimimum and maximum allowed files and minimum or maximum files reached.
        /// </summary>
        [Parameter] public bool HideInformativeLabelText { get; set; }
        /// <summary>
        /// Adds an informative label that is required.
        /// </summary>
        [Parameter] public bool Required { get; set; }
        /// <summary>
        /// Adds an informative label that is required.
        /// </summary>
        [Parameter] public bool ForceUpload { get; set; }
        [Inject] public virtual IFileReaderService FileReaderService { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Inject] public virtual IApplicationState ApplicationState { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        public HttpBaseUserBlobsService<UserBlobsViewModel, Identifier<string>, string> HttpBaseUserBlobsService { get; set; }
        public ElementReference DropTarget { get; set; }
        public ElementReference DropTargetInput { get; set; }
        public IFileReaderRef DropReference { get; set; }
        public IFileReaderRef DropInputReference { get; set; }
        public string DropClass { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HttpBaseUserBlobsService = new HttpBaseUserBlobsService<UserBlobsViewModel, Identifier<string>, string>()
                {
                    HttpClient = HttpClient
                };
                if (!string.IsNullOrWhiteSpace(Api))
                {
                    HttpBaseUserBlobsService.Api = Api;
                }
                if (!string.IsNullOrWhiteSpace(ControllerName))
                {
                    HttpBaseUserBlobsService.ControllerName = ControllerName;
                }
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
                    DropZoneText = (MarkupString)LanguageLocalizer["DropFilesOrClickHere"];
                }
                if (string.IsNullOrWhiteSpace(ResetButtonText.Value))
                {
                    ResetButtonText = (MarkupString)LanguageLocalizer["Reset"];
                }
                if (string.IsNullOrWhiteSpace(AddButtonText.Value))
                {
                    AddButtonText = (MarkupString)LanguageLocalizer["Add"];
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
                DropReference = FileReaderService.CreateReference(DropTarget);
                await DropReference.RegisterDropEventsAsync(AllowMultiple);
                StateHasChanged();
            }
        }
        public virtual async Task Clear()
        {
            await DropReference.ClearValue();
            await DropInputReference.ClearValue();
            var blobDatas = new List<BlobData>();
            BlobDatas = blobDatas;
            await BlobDatasChanged.InvokeAsync(blobDatas);
            StateHasChanged();
        }
        public virtual async Task OpenDeviceFileSystem()
        {
            if (!PreventAdding && CanAddMoreFiles())
            {
                await JSRuntime.InvokeVoidAsync("eval", $"document.querySelector('[_bl_{DropTargetInput.Id}]').click()");
            }
        }
        public virtual async Task Remove(BlobData blobData)
        {
            if (!HideDeleteButton && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToDeleteTheFile"],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                var blobDatas = new List<BlobData>(BlobDatas);
                blobDatas.Remove(blobData);
                BlobDatas = blobDatas;
                await BlobDatasChanged.InvokeAsync(blobDatas);
            }
            await UpdateMaximunMinimunReachedFiles();
            StateHasChanged();
        }
        public virtual async Task Download(BlobData blobData)
        {
            if (!PreventDownload && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToDownloadTheFile"],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await BlazorDownloadFileService.DownloadFile(blobData.Name, blobData.Data, "application/octect-stream");
            }
        }
        public virtual async Task Remove(UserBlobs userBlob)
        {
            if (!HideDeleteButton && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToDeleteTheFile"],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                var userBlobs = new List<UserBlobs>(UserBlobs);
                userBlobs.Remove(userBlob);
                UserBlobs = userBlobs;
                await UserBlobsChanged.InvokeAsync(userBlobs);
            }
            await UpdateMaximunMinimunReachedFiles();
            StateHasChanged();
        }
        public virtual async Task Download(UserBlobs userBlobs)
        {
            if (!PreventDownload && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToDownloadTheFile"],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () =>
                {
                    var Response = await HttpBaseUserBlobsService.DownloadBinaryAsync(new Identifier<string>(userBlobs.UserBlobId), new IdentifierStringValidator());
                    await SweetAlertService.HideLoaderAsync();
                    if (Response.IsNull())
                    {
                        await SweetAlertService.FireAsync(LanguageLocalizer["UnknownErrorOccurred"], LanguageLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
                    }
                    else if (Response.IsNotNull() &&
                            Response.HttpResponseMessage.IsNotNull() &&
                            Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await SweetAlertService.FireAsync(LanguageLocalizer["SessionExpired"], LanguageLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
                        await ApplicationState.LogoutAndNavigateTo("/login");
                    }
                    else if (Response.IsNotNull() &&
                            Response.Succeed &&
                            Response.Response.IsNotNull() &&
                            Response.HttpResponseMessage.IsNotNull() &&
                            Response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        await BlazorDownloadFileService.DownloadFile(userBlobs.FileName, await Response.Response.Content.ReadAsStreamAsync(), "application/octect-stream");
                    }
                    else if (Response.IsNotNull() &&
                            Response.HasException)
                    {
                        await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
                    }
                    else
                    {
                        await SweetAlertService.FireAsync(LanguageLocalizer["UnknownErrorOccurred"], LanguageLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Error);
                    }
                }), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["DownloadingFile"]);
                StateHasChanged();
            }
        }
        public virtual void OnDragEnter(EventArgs e)
        {
            DropClass = $"{DropTargetClass} {DropTargetDragClass}";
            StateHasChanged();
        }
        public virtual async Task OnDrop(EventArgs e)
        {
            if (!PreventAdding && CanAddMoreFiles())
            {
                await ReadFiles();
            }
        }
        public virtual void OnDragLeave(EventArgs e)
        {
            DropClass = DropTargetClass;
            StateHasChanged();
        }
        public virtual bool CanAddMoreFiles()
        {
            if (MaximumAllowedFiles <= 0)
            {
                return true;
            }
            return MaximumAllowedFiles > FilesCount();
        }
        public virtual MarkupString AllowsUpToFilesText()
        {
            return (MarkupString)(MaximumAllowedFiles <= 0 ? "" : $@"{LanguageLocalizer["AllowsUpTo"]} {MaximumAllowedFiles} {(MaximumAllowedFiles == 1 ? LanguageLocalizer["File"] : LanguageLocalizer["Files"])}");
        }
        public virtual MarkupString RequiredText()
        {
            return (MarkupString)(Required ? LanguageLocalizer["FileUploadRequired"] + ", " : "");
        }
        public virtual MarkupString ForceUploadText()
        {
            return (MarkupString)(ForceUpload ? LanguageLocalizer["UploadingANewFileIsRequired"] + ", " : "");
        }
        public virtual MarkupString InformativeLabelText()
        {
            if (!MinimumAllowedFilesReached && !MaximumAllowedFilesReached && MaxFileSize > 0 && !string.IsNullOrWhiteSpace(MaxFileSizeMeasuredText))
            {
                return (MarkupString)$@"<div class=""text-muted""><small>{ForceUploadText()}{RequiredText()}{AllowsUpToFilesText()}, {LanguageLocalizer["MaximumSizePerFile"]} {MaxFileSizeMeasuredText}</small></div>";
            }
            else if (MinimumAllowedFilesReached && !MaximumAllowedFilesReached && MaxFileSize > 0 && !string.IsNullOrWhiteSpace(MaxFileSizeMeasuredText))
            {
                return (MarkupString)$@"<div class=""text-muted""><small>{ForceUploadText()}{RequiredText()}{AllowsUpToFilesText()}, {LanguageLocalizer["MaximumSizePerFile"]} {MaxFileSizeMeasuredText}, {LanguageLocalizer["MinimumReached"]}</small></div>";
            }
            else if (MaximumAllowedFilesReached && MaxFileSize > 0 && !string.IsNullOrWhiteSpace(MaxFileSizeMeasuredText))
            {
                return (MarkupString)$@"<div class=""text-muted""><small>{ForceUploadText()}{RequiredText()}{AllowsUpToFilesText()}, {LanguageLocalizer["MaximumSizePerFile"]} {MaxFileSizeMeasuredText}, {LanguageLocalizer["MaximunReached"]}</small></div>";
            }
            else if (MinimumAllowedFilesReached && !MaximumAllowedFilesReached && MaxFileSize <= 0)
            {
                return (MarkupString)$@"<div class=""text-muted""><small>{ForceUploadText()}{RequiredText()}{AllowsUpToFilesText()}, {LanguageLocalizer["MinimumReached"]}</small></div>";
            }
            else if (MaximumAllowedFilesReached && MaxFileSize <= 0)
            {
                return (MarkupString)$@"<div class=""text-muted""><small>{ForceUploadText()}{RequiredText()}{AllowsUpToFilesText()}, {LanguageLocalizer["MaximunReached"]}</small></div>";
            }
            else
            {
                return (MarkupString)"";
            }
        }
        public virtual int FilesCount()
        {
            var currentBlobsCount = 0;
            currentBlobsCount += BlobDatas.IsNull() || !BlobDatas.Any() ? 0 : BlobDatas.Count();
            currentBlobsCount += UserBlobs.IsNull() || !UserBlobs.Any() ? 0 : UserBlobs.Count();
            return currentBlobsCount;
        }
        public virtual async Task UpdateMaximunMinimunReachedFiles()
        {
            var filesCount = FilesCount();
            MinimumAllowedFilesReached = filesCount >= MinimumAllowedFiles;
            await MinimumAllowedFilesReachedChanged.InvokeAsync(MinimumAllowedFilesReached);
            MaximumAllowedFilesReached = filesCount >= MaximumAllowedFiles;
            await MaximumAllowedFilesReachedChanged.InvokeAsync(MaximumAllowedFilesReached);
            DropInputReference = FileReaderService.CreateReference(DropTargetInput);
        }
        public virtual async Task OnInputChange(EventArgs e)
        {
            if (!PreventAdding && CanAddMoreFiles())
            {
                await ReadFiles();
            }
        }
        public virtual async Task ReadFiles()
        {
            DropClass = DropTargetClass;
            StateHasChanged();
            var blobDatas = new ObservableRangeCollection<BlobData>();
            if (AllowMultiple)
            {
                var fileReferences = await DropReference.EnumerateFilesAsync();
                fileReferences = fileReferences.Concat(await DropInputReference.EnumerateFilesAsync());
                var filesCount = FilesCount();
                if (MaximumAllowedFiles > 0 && (fileReferences.Count() + filesCount) > MaximumAllowedFiles)
                {
                    await DropReference.ClearValue();
                    await DropInputReference.ClearValue();
                    await SweetAlertService.FireAsync(LanguageLocalizer["MaximumAllowedFilesExceeded"], LanguageLocalizer["TheSumOfSelectedFilesExceedsTheMaximumAllowedFiles"], SweetAlertIcon.Warning);
                    return;
                }
                foreach (var fileReference in fileReferences)
                {
                    var fileInfo = await fileReference.ReadFileInfoAsync();
                    if (fileInfo.Size > MaxFileSize)
                    {
                        await SweetAlertService.FireAsync(LanguageLocalizer["MaxFileSizeExceeded"], fileInfo.Name, SweetAlertIcon.Warning);
                    }
                    else
                    {
                        var blobData = new BlobData
                        {
                            Name = fileInfo.Name,
                            Size = fileInfo.Size,
                            Type = fileInfo.Type,
                            InputName = InputName,
                            LastModified = fileInfo.LastModifiedDate ?? default
                        };
                        var stream = BufferSize <= 0 ? await fileReference.CreateMemoryStreamAsync() : await fileReference.CreateMemoryStreamAsync(BufferSize);
                        blobData.Data = stream.ToArray();
                        blobDatas.Add(blobData);
                    }
                }
            }
            else
            {
                var fileReferences = await DropReference.EnumerateFilesAsync();
                if (fileReferences.IsNull() || !fileReferences.Any())
                {
                    fileReferences = await DropInputReference.EnumerateFilesAsync();
                }
                foreach (var fileReference in fileReferences)
                {
                    var fileInfo = await fileReference.ReadFileInfoAsync();
                    if (fileInfo.Size > MaxFileSize)
                    {
                        await SweetAlertService.FireAsync(LanguageLocalizer["MaxFileSizeExceeded"], fileInfo.Name, SweetAlertIcon.Warning);
                    }
                    else
                    {
                        var blobData = new BlobData
                        {
                            Name = fileInfo.Name,
                            Size = fileInfo.Size,
                            Type = fileInfo.Type,
                            InputName = InputName,
                            LastModified = fileInfo.LastModifiedDate ?? default
                        };
                        var stream = BufferSize <= 0 ? await fileReference.CreateMemoryStreamAsync() : await fileReference.CreateMemoryStreamAsync(BufferSize);
                        blobData.Data = stream.ToArray();
                        blobDatas.Add(blobData);
                    }
                    break;
                }
            }
            if (BlobDatas.IsNull() || !BlobDatas.Any() || !AllowMultiple)
            {
                BlobDatas = blobDatas;
            }
            else
            {
                foreach (var blobData in BlobDatas.ToList())
                {
                    if (blobDatas.Any(w => w.Name == blobData.Name &&
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
            await DropReference.ClearValue();
            await DropInputReference.ClearValue();
            await UpdateMaximunMinimunReachedFiles();
            StateHasChanged();
        }
    }
}
