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
        public static string TransactionTypeMessage<TEntity>(this TransactionType transactionType)
        {
            if (transactionType == TransactionType.Add)
            {
                return "Added a " + typeof(TEntity).Name;
            }
            else if (transactionType == TransactionType.Delete)
            {
                return "Deleted a " + typeof(TEntity).Name;
            }
            else if (transactionType == TransactionType.SoftDelete)
            {
                return "Soft Deleted a " + typeof(TEntity).Name;
            }
            else if (transactionType == TransactionType.SoftUndelete)
            {
                return "Soft Undeleted a " + typeof(TEntity).Name;
            }
            else if (transactionType == TransactionType.Update)
            {
                return "Updated a " + typeof(TEntity).Name;
            }
            else if (transactionType == TransactionType.Search)
            {
                return "Searched a " + typeof(TEntity).Name;
            }
            throw new ArgumentException("No transaction type found");
        }
    }
}
