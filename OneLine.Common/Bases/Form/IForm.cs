namespace OneLine.Bases
{
    public interface IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModelable<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        IBlobDataCollectionable<TBlobData>,
        IApiResponseable<T>,
        IApiResponseableCollectionable<T>,
        IApiResponseableBlobable<T, TUserBlobs>,
        IConfigurable,
        ISaveable,
        IValidatable,
        IFormStateable,
        IFormModeable,
        ICollectionAppendableReplaceableModeable,
        ILoadable,
        IDeletable,
        IResettable,
        ICancelable
    {
    }
}
