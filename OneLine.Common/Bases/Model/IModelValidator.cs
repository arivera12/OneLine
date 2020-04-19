namespace OneLine.Bases
{
    public interface IModelValidator<TValidator, TIdentifierValidator>
    {
        TValidator Validator { get; set; }
        TIdentifierValidator IdentifierValidator { get; set; }
    }
}