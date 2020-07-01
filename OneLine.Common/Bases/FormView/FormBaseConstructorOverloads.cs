using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneLine.Bases
{
    public abstract partial class FormViewBase<T, TIdentifier, TId, THttpService> :
        IFormView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public FormViewBase()
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
        }
        public FormViewBase(FormState formState)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormViewBase(FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
        }
        public FormViewBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.AddRange(records);
            HttpService = new THttpService();
        }
        public FormViewBase(FormState formState, FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
        }
        public FormViewBase(FormState formState, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(FormState formState, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormState formState, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormState formState, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormViewBase(FormState formState, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
        }
        public FormViewBase(FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(FormMode formMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormMode formMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormMode formMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormViewBase(FormMode formMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormMode = formMode;
        }
        public FormViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier, bool autoLoad = false)
        {
            Identifier = identifier;
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers, bool autoLoad = false)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
            AutoLoad = autoLoad;
            if (AutoLoad)
            {
                _ = new Action(async () => await Load());
            }
        }
        public FormViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormViewBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = Enumerable.Empty<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>() { AllowDuplicates = AllowDuplicates };
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
    }
}
