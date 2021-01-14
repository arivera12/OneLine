namespace OneLine.Contracts
{
    public interface IHelpableApiContext
    {
        /// <summary>
        /// Helper method that gets table's primary key by the model builder definition
        /// </summary>
        /// <returns></returns>
        string GetTablePrimaryKeyFieldName();
    }
}
