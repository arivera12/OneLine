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
        ISaveableWithValidator,
        IFormStateable,
        IFormModeable,
        ICollectionAppendableReplaceableModeable,
        ILoadableApiResponseable<T>,
        ILoadableApiResponseableCollectionable<T>,
        IDeletableWithValidation<T>,
        IResettable,
        ICancelable
    {
    }
}
