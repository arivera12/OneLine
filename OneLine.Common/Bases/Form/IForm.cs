namespace OneLine.Bases
{
    public interface IForm<T, TValidator, TIdentifier, TIdentifierValidator, THttpService, TSearchExtraParams, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IModelValidator<TValidator, TIdentifierValidator>,
        IHttpServiceable<THttpService>,
        ISearchExtraParams<TSearchExtraParams>,
        IBlobData<TBlobData>,
        IApiResponseable<T>,
        IApiResponseableWithBlobs<T, TUserBlobs>,
        IConfigurable,
        ISaveable,
        ILoadableApiResponseable<T>,
        IDeletable<T>,
        IResettable,
        ICancelable
    {
    }
}
