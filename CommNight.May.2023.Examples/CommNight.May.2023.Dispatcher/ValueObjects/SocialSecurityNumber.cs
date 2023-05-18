using ActiveLogin.Identity.Swedish;

namespace CommNight.May._2023.Domain.ValueObjects
{
    public sealed class SocialSecurityNumber
    {
        public string Value { get; }

        public SocialSecurityNumber(string value)
        {
            var pin = PersonalIdentityNumber.Parse(value, StrictMode.TenOrTwelveDigits);

            Value = pin.To12DigitString();
        }
    }
}