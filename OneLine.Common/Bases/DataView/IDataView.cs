namespace OneLine.Bases
{
    public interface IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        ISearchExtraParams<object>,
        ISearchable,
        ISelectable<T>,
        IPageable,
        IApiResponsePaged<T>,
        IConfigurable
    {  
    }
}
