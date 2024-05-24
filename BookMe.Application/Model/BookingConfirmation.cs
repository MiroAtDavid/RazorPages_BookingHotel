namespace BookMe.Model;

public record BookingConfirmation
{
    public Booking Booking { get; init; } 
    public string ConfirmationCode { get; init; }
}
