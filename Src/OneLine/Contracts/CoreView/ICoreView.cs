using OneLine.Services;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines a base core view with most common actions and options
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="THttpService"></typeparam>
    public interface ICoreView<T, TIdentifier, THttpService> :
        IModelable<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        ISearchExtraParameterable<object>,
        ISearchableEventable,
        ICollectionFilterableSortable<T>,
        ICollectionAppendableReplaceableModeable,
        ISelectable<T>,
        IPageable,
        ISearchablePageable,
        IPageableNavigable,
        ISortable,
        IPropertySortable,
        IApiResponseable<T>,
        IApiResponseablePageable<T>,
        IApiResponseableCollectionable<T>,
        ILoadableEventable,
        IMutableBlobDataCollectionableValidatable,
        ISaveableEventable,
        IValidatableEventable,
        IFormStateable,
        IFormModeable,
        IDeletableEventable,
        IResettableEventable,
        ICancelableEventable
    {
        /// <summary>
        /// A save file service
        /// </summary>
        public ISaveFile SaveFile { get; set; }
        /// <summary>
        /// A service which identifies the current device
        /// </summary>
        public IDevice Device { get; set; }
        /// <summary>
        /// A translator service using the resource manager
        /// </summary>
        public IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        /// <summary>
        /// A base application state
        /// </summary>
        public IApplicationState ApplicationState { get; set; }
        /// <summary>
        /// A service which manage the configuration file
        /// </summary>
        public IApplicationConfiguration ApplicationConfiguration { get; set; }
        /// <summary>
        /// A service which manage the storage of a device
        /// </summary>
        public IDeviceStorage DeviceStorage { get; set; }
    }
}
