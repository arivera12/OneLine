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
        ISaveableEventable,
        IValidatableEventable,
        IFormStateable,
        IFormModeable,
        ICollectionAppendableReplaceableModeable,
        ILoadable,
        IDeletableEventable,
        IResettableEventable,
        ICancelableEventable
    {
    }
}
