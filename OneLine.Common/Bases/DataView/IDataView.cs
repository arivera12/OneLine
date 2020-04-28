namespace OneLine.Bases
{
    public interface IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        ISearchExtraParams<object>,
        ISearchable,
        ICollectionAppendReplaceModeable,
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
