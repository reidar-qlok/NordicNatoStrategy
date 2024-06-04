namespace NordicNatoStrategy.Strategy
{
    public interface IPersonalNumberValidationStrategy
    {
        bool Validate(string personalNumber);
    }
}
