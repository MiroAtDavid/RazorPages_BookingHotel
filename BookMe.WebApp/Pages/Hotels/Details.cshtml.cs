using System.Globalization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookMe.Dto;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

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
    public Guid guid { get; set; }
    
    public Hotel? Hotel { get; set; }
    
    public Booking NewBooking { get; set; }

    public IReadOnlyList<Booking> Bookings { get; private set; } = new List<Booking>();
    public Dictionary<Guid, BookingDto> EditBokings { get; set; } = new();
    public Dictionary<Guid, bool> BookingsToDelete { get; set; } = new();
    public IEnumerable<SelectListItem> RoomSelectList =>
        _room.Set.OrderBy(r => r.RoomType).Select(p => new SelectListItem(p.RoomType, p.Id.ToString()));
    
    public IActionResult OnPostEditBooking(Guid id, Guid bookingId, Dictionary<Guid, BookingDto> editbookings) {
        if (!ModelState.IsValid) { return Page(); }
        var booking = _bookings.FindById(bookingId);
        if (booking is null) { return RedirectToPage(); }
        _mapper.Map(editbookings[bookingId], booking);
        var (success, message) = _bookings.Update(booking);
        if (!success) {
            ModelState.AddModelError("", message!);
            return Page();
        }
        return RedirectToPage();
    }
    

    
    public IActionResult OnGet(Guid guid) {
        //var hotel = _hotels.FindById(guid);
        var hotel = _hotels.Set
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Room)
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Guest) 
            .FirstOrDefault(h => h.Id == guid);
        if (hotel == null)
            return RedirectToPage("Hotels/Index");
        Hotel = hotel;
        Bookings = hotel.Bookings.ToList();
        return Page();
    }
    /*
    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
        // SELECT * FROM Stores INNER JOIN Offers ON (...)
        // INNER JOIN Product ON (...)
        var hotel = _hotels.Set
            .Include(h => h.Bookings)
            //.ThenInclude(b => b.Room)
            .FirstOrDefault(h => h.Id == guid);
        if (hotel is null) {
            context.Result = RedirectToPage("/Hotels/Index");
            return;
        }
        Hotel = hotel;
        Bookings = hotel.Bookings.ToList();
        BookingsToDelete = Bookings.ToDictionary(b => b.Id, o => false);
        //EditBokings = _bookings.Set.Where(b => b.Hotel.Id == Guid)
        //    .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
        //    .ToDictionary(b => b.Id, b => b);
    }
    */
}