using System.Text.RegularExpressions;

namespace CommNight.May._2023.CodeContainers
{
    public class RegNrValidator
    {
        public bool IsValid(string regNr)
        {
            var formattedValue = regNr.Trim().Replace(" ", string.Empty);

            var regNrRegex = new Regex("[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}");

            return regNrRegex.IsMatch(formattedValue);
        }
    }
}