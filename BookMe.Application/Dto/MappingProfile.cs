using BookMe.Model;
using AutoMapper;
using System;

namespace BookMe.Dto;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<HotelDto, Hotel>();  // HotelDto --> Hotel
        CreateMap<Hotel, HotelDto>();  // Hotel --> HotelDto
        CreateMap<BookingDto, Booking>()
            .ForMember(
                o => o.Id, 
                opt => opt.MapFrom(o => o.Id == default ? Guid.NewGuid() : o.Id));
        CreateMap<Booking, BookingDto>();
    }
}