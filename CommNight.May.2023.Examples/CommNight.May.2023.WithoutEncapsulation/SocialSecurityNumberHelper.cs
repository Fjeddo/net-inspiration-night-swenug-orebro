using ActiveLogin.Identity.Swedish;

namespace CommNight.May._2023.CodeContainers;

public class SocialSecurityNumberHelper
{
    public bool IsValid(string socialSecurityNumber)
    {
        return PersonalIdentityNumber.TryParse(socialSecurityNumber, StrictMode.TenOrTwelveDigits, out _);
    }

    public string Format(string socialSecurityNumber)
    {
        var pin = PersonalIdentityNumber.Parse(socialSecurityNumber, StrictMode.TenOrTwelveDigits);

        return pin.To12DigitString();
    }
}