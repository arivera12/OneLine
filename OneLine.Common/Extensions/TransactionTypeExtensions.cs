using OneLine.Enums;
using System;

namespace OneLine.Extensions
{
    public static class TransactionTypeExtensions
    {
        /// <summary>
        /// Returns a generic audit trail transaction message
        /// </summary>
        /// <returns></returns>
        public static string TransactionTypeMessage<T>(this TransactionType transactionType)
        {
            if (transactionType.Equals(TransactionType.Add))
            {
                return "Added a " + typeof(T).Name;
            }
            else if (transactionType.Equals(TransactionType.Delete))
            {
                return "Deleted a " + typeof(T).Name;
            }
            else if (transactionType.Equals(TransactionType.SoftDelete))
            {
                return "Soft Deleted a " + typeof(T).Name;
            }
            else if (transactionType.Equals(TransactionType.SoftUndelete))
            {
                return "Soft Undeleted a " + typeof(T).Name;
            }
            else if (transactionType.Equals(TransactionType.Update))
            {
                return "Updated a " + typeof(T).Name;
            }
            else if (transactionType.Equals(TransactionType.Search))
            {
                return "Searched a " + typeof(T).Name;
            }
            throw new ArgumentException("No transaction type found");
        }
    }
}
