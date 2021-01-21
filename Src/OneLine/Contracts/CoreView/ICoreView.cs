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
        /// <inheritdoc/>
        public ISaveFile SaveFile { get; set; }
        /// <inheritdoc/>
        public IDevice Device { get; set; }
        /// <inheritdoc/>
        public IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        /// <inheritdoc/>
        public IApplicationState ApplicationState { get; set; }
        /// <inheritdoc/>
        public IApplicationConfiguration ApplicationConfiguration { get; set; }
        /// <inheritdoc/>
        public IDeviceStorage DeviceStorage { get; set; }
    }
}
