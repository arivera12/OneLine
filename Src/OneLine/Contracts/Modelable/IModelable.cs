using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Contracts
{
    /// <summary>
    /// Defines properties that are holder for the models context of a class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IModelable<T, TIdentifier>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        TIdentifier Identifier { get; set; }
        /// <summary>
        /// The identifier changed action callback
        /// </summary>
        Action<TIdentifier> IdentifierChanged { get; set; }
        /// <summary>
        /// The identifiers
        /// </summary>
        IEnumerable<TIdentifier> Identifiers { get; set; }
        /// <summary>
        /// The identifiers changed action callback
        /// </summary>
        Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        /// <summary>
        /// The context record
        /// </summary>
        T Record { get; set; }
        /// <summary>
        /// The context record changed action callback
        /// </summary>
        Action<T> RecordChanged { get; set; }
        /// <summary>
        /// The context records
        /// </summary>
        ObservableRangeCollection<T> Records { get; set; }
        /// <summary>
        /// The context records changed action callback
        /// </summary>
        Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        /// <summary>
        /// Whether to allow duplicates on <see cref="Records"/>
        /// </summary>
        bool AllowDuplicates { get; set; }
    }
}