using BookMe.Model;
using AutoMapper;
using System;

namespace BookMe.Dto;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<HotelDto, Hotel>();  // HotelDto --> Hotel
        CreateMap<Hotel, HotelDto>();  // Hotel --> HotelDto
        CreateMap<BookingDto, Booking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Booking, BookingDto>();

    }
    
}