using BookMe.Model;
using Address = Bogus.DataSets.Address;

namespace BookMe.Dto;

// TODO verification
public record HotelDto(
        Guid Id,
        string Name,
        Stars Stars,
        Address Address,
        int? BookingsCount
    );