using System.Text.RegularExpressions;

namespace CommNight.May._2023.Domain.ValueObjects;

public sealed class RegNr
{
    public string Value { get; }

    public RegNr(string value)
    {
        var formattedValue = value.Trim().Replace(" ", string.Empty);
                
        var regNrRegex = new Regex("[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}");

        if (!regNrRegex.IsMatch(formattedValue))
        {
            throw new ArgumentException($"The specified value {value} is not a valid Registration number.");
        }

        Value = formattedValue;
    }
}