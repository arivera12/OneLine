namespace OneLine.Bases
{
    public interface IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
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
        ILoadable
    {  
    }
}
