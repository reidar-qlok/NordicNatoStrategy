namespace NordicNatoStrategy.Strategy
{
    public class NorwegianPersonalNumberValidation : IPersonalNumberValidationStrategy
    {
        public bool Validate(string personalNumber)
        {
            // Kontrollera att personnumret är exakt 11 siffror långt
            if (personalNumber.Length != 11 || !personalNumber.All(char.IsDigit))
                return false;

            // Kontrollera att födelsedatumet är giltigt (DDMMYY)
            string datePart = personalNumber.Substring(0, 6);
            if (!IsValidDate(datePart))
                return false;

            // Kontrollera kontrollsiffrorna
            return HasValidControlDigits(personalNumber);
        }

        private bool IsValidDate(string datePart)
        {
            // Kontrollera att datumdelen kan konverteras till ett giltigt datum
            if (!DateTime.TryParseExact(datePart, "ddMMyy", null, System.Globalization.DateTimeStyles.None, out _))
                return false;

            return true;
        }

        private bool HasValidControlDigits(string personalNumber)
        {
            int[] weights1 = { 3, 7, 6, 1, 8, 9, 4, 5, 2 };
            int[] weights2 = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            int k1 = CalculateControlDigit(personalNumber, weights1, 9);
            int k2 = CalculateControlDigit(personalNumber, weights2, 10);

            return k1 == int.Parse(personalNumber[9].ToString()) && k2 == int.Parse(personalNumber[10].ToString());
        }

        private int CalculateControlDigit(string number, int[] weights, int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += int.Parse(number[i].ToString()) * weights[i];
            }
            int remainder = sum % 11;
            return remainder == 0 ? 0 : 11 - remainder;
        }
    }
}
