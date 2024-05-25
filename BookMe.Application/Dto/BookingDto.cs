using BookMe.Model;

namespace BookMe.Dto;

// TODO verification
public record BookingDto(
        Guid Id,
        DateTime DateTime,
        Guid GuestId,
        int RoomId,
        int BookingDuration
    );

