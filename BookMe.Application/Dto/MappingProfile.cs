using BookMe.Model;
using AutoMapper;
using System;

namespace BookMe.Dto;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<HotelDto, Hotel>();  // HotelDto --> Hotel
        CreateMap<Hotel, HotelDto>();  // Hotel --> HotelDto
        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

        CreateMap<Booking, BookingDto>();
    }
}