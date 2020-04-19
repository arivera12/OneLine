namespace OneLine.Bases
{
    public interface IListView<T, TValidator, TIdentifier, TIdentifierValidator, THttpService, TSearchExtraParams, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IModelValidator<TValidator, TIdentifierValidator>,
        IHttpServiceable<THttpService>,
        ISearchExtraParams<TSearchExtraParams>,
        ISearchable,
        ISelectable<T>,
        IPageable,
        IApiResponsePaged<T>,
        IConfigurable
    {  
    }
}
