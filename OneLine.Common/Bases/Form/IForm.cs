namespace OneLine.Bases
{
    public interface IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        IBlobData<TBlobData>,
        IApiResponseable<T>,
        IApiResponseableCollectionable<T>,
        IApiResponseableBlobable<T, TUserBlobs>,
        IConfigurable,
        ISaveableWithValidator,
        IFormStateable,
        IFormModeable,
        ICollectionAppendReplaceModeable,
        ILoadableApiResponseable<T>,
        ILoadableApiResponseableCollectionable<T>,
        IDeletableWithValidation<T>,
        IResettable,
        ICancelable
    {
    }
}
