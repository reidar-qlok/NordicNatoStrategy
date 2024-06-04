using System.Globalization;

namespace NordicNatoStrategy.Strategy
{
    public class FinnishPersonalNumberValidation : IPersonalNumberValidationStrategy
    {
        public bool Validate(string personalNumber)
        {
            // Kontrollera att personnumret är exakt 11 tecken långt
            if (personalNumber.Length != 11)
                return false;

            // Kontrollera att de första sex tecknen är ett giltigt datum
            string datePart = personalNumber.Substring(0, 6);
            char separator = personalNumber[6];
            if (!IsValidDate(datePart, separator))
                return false;

            // Kontrollera att de sista fyra tecknen består av siffror och en giltig kontrollsiffra
            string individualNumber = personalNumber.Substring(7, 3);
            char controlCharacter = personalNumber[10];
            return IsValidControlCharacter(datePart, individualNumber, controlCharacter);
        }

        private bool IsValidDate(string datePart, char separator)
        {
            string century;
            switch (separator)
            {
                case '+':
                    century = "18";
                    break;
                case '-':
                    century = "19";
                    break;
                case 'A':
                    century = "20";
                    break;
                default:
                    return false;
            }

            string fullDate = century + datePart;
            return DateTime.TryParseExact(fullDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool IsValidControlCharacter(string datePart, string individualNumber, char controlCharacter)
        {
            string numberPart = datePart + individualNumber;
            if (!int.TryParse(numberPart, out int number))
                return false;

            string validCharacters = "0123456789ABCDEFHJKLMNPRSTUVWXY";
            int remainder = number % 31;
            return validCharacters[remainder] == controlCharacter;
        }
    }
}
