﻿namespace OneLine.Bases
{
    public interface IForm<T, TIdentifier, THttpService> :
        IModelable<T, TIdentifier>,
        IHttpServiceable<THttpService>,
        IMutableBlobDataCollectionableValidatable,
        IApiResponseable<T>,
        IApiResponseableCollectionable<T>,
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
