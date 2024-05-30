using System.ComponentModel.DataAnnotations;
using BookMe.Model;

namespace BookMe.Dto;

// TODO verification
public record BookingDto(
        Guid Id,
        [FutureDate]
        DateTime Date,
        Guid GuestId,
        Guid HotelId,
        int RoomId,
        [Range(1, int.MaxValue, ErrorMessage = "Booking duration must be a positive number.")]
        int BookingDuration
    );

public class FutureDateAttribute : ValidationAttribute {
    public FutureDateAttribute() : base("The date must be in the future.") { }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        if (value is DateTime dateTime) {
            if (dateTime > DateTime.Now) {
                return ValidationResult.Success;
            } else {
                return new ValidationResult(ErrorMessage);
            }
        }
        return new ValidationResult("Invalid date format.");
    }
}