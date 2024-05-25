using AutoMapper;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMe.WebApp.Pages.Hotels;

public class Details : PageModel {

    private readonly HotelRepository _hotels;
    private readonly BookingRepository _bookings;
    private readonly GuestRepository _guests;
    private readonly AddressRepository _addresses;
    private readonly EmployeeRepository _employees;
    private readonly RoomRepository _room;
    private readonly IMapper _mapper;

    public Details(IMapper mapper,
        HotelRepository hotels,
        BookingRepository bookings,
        GuestRepository guests,
        AddressRepository addresses,
        EmployeeRepository employees,
        RoomRepository rooms
    ) {
        _mapper = mapper;
        _hotels = hotels;
        _bookings = bookings;
        _guests = guests;
        _addresses = addresses;
        _employees = employees;
        _room = rooms;
    }
    
    [FromRoute]
    public Guid Id { get; set; }
    public Booking NewBooking { get; set; }
    public Hotel Hotel { get; set; }
    
    public void OnGet() {
        
    }
}