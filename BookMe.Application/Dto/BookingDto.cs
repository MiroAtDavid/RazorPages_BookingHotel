using BookMe.Model;

namespace BookMe.Dto;

public record BookingDto(
        Guid Id,
        DateTime DateTime,
        Guid GuestId,
        int RoomId,
        int BookingDuration
    );

