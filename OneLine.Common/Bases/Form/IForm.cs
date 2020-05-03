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
        ISaveableValidatable,
        IFormStateable,
        IFormModeable,
        ICollectionAppendableReplaceableModeable,
        ILoadable,
        IDeletableValidatable,
        IResettable,
        ICancelable
    {
    }
}
