namespace OneLine.Bases
{
    public interface ICoreView<T, TIdentifier, THttpService> :
        IModelable<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        ISearchExtraParameterable<object[]>,
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
        IConfigurable,
        ILoadable,
        IMutableBlobDataCollectionableValidatable,
        ISaveableEventable,
        IValidatableEventable,
        IFormStateable,
        IFormModeable,
        IDeletableEventable,
        IResettableEventable,
        ICancelableEventable
    {
    }
}
