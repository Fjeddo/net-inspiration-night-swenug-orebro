namespace CommNight.May._2023.Domain.ValueObjects;

public sealed class TicketId
{
    public Guid Value { get; }

    public TicketId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                $"The specified value {value} is not a valid TicketId. The value must be the non-empty GUID.");
        }

        Value = value;
    }

    public static TicketId NewId() => new(Guid.NewGuid());
}