using NordicNatoStrategy.Strategy;

namespace NordicNatoStrategy.Models
{
    public class PersonalNumberValidatior
    {
        private IPersonalNumberValidationStrategy _validationStrategy;

        public void SetValidationStrategy(IPersonalNumberValidationStrategy validationStrategy)
        {
            _validationStrategy = validationStrategy;
        }

        public bool ValidatePersonalNumber(string personalNumber)
        {
            return _validationStrategy.Validate(personalNumber);
        }
    }
}
