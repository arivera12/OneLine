namespace OneLine.Bases
{
    public interface IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IModel<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        IBlobData<TBlobData>,
        IApiResponseable<T>,
        IApiResponseableWithBlobs<T, TUserBlobs>,
        IConfigurable,
        ISaveableWithValidator,
        ILoadableApiResponseable<T>,
        IDeletableWithValidation<T>,
        IResettable,
        ICancelable
    {
    }
}
