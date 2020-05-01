namespace OneLine.Bases
{
    public interface IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModelable<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        ISearchExtraParameterable<object>,
        ISearchable,
        ICollectionFilterableSortable<T>,
        ICollectionAppendableReplaceableModeable,
        ISelectable<T>,
        IPageable,
        IApiResponseable<T>,
        IApiResponseablePageable<T>,
        IApiResponseableCollectionable<T>,
        IConfigurable,
        ILoadable<T>
    {  
    }
}
