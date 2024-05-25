using BookMe.Model;
using Address = Bogus.DataSets.Address;

namespace BookMe.Dto;

public record HotelDto(
        Guid Guid,
        string Name,
        Stars Stars,
        Address Address
    );