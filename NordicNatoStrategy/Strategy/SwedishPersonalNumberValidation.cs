using System.Globalization;

namespace NordicNatoStrategy.Strategy
{
    public class SwedishPersonalNumberValidation : IPersonalNumberValidationStrategy
    {
        public bool Validate(string personalNumber)
        {
            if (personalNumber.Length != 12)
                return false;

            // Kontrollera att datumdelen är giltig (ÅÅÅÅMMDD)
            string datePart = personalNumber.Substring(0, 8);
            if (!DateTime.TryParseExact(datePart, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                return false;


            if (!long.TryParse(personalNumber, out _))
                return false;

            // Kontrollsiffra validering med Luhn-algoritmen
            return IsValidLuhn(personalNumber.Substring(2)); // Använd bara de sista 10 siffrorna för Luhn-algoritmen

            return true;
        }
        private bool IsValidLuhn(string number)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char[] nx = number.ToCharArray();
                int n = int.Parse(nx[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n = n % 10 + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }
}
