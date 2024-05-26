using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

public class Delete : PageModel {
    private readonly HotelRepository _hotels;
    private readonly BookingRepository _bookings;
    private readonly GuestRepository _guests;
    private readonly AddressRepository _addresses;
    private readonly EmployeeRepository _employees;
    private readonly RoomRepository _room;
    private readonly IMapper _mapper;

    public Delete(IMapper mapper,
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

    [FromRoute] public Guid Guid { get; set; }

    public Hotel? Hotel { get; set; }
    public string? Message { get; set; }
    public Booking NewBooking { get; set; }
    public IReadOnlyList<Booking> Bookings { get; private set; } = new List<Booking>();
    public Dictionary<Guid, BookingDto> EditBokings { get; set; } = new();
    public Dictionary<Guid, bool> BookingsToDelete { get; set; } = new();
    public IEnumerable<SelectListItem> RoomSelectList =>
        _room.Set.OrderBy(r => r.RoomType).Select(p => new SelectListItem(p.RoomType, p.Id.ToString()));
    public IEnumerable<SelectListItem> GuestSelectListI =>
        _guests.Set.OrderBy(g => g.Email).Select(p => new SelectListItem(p.Email, p.Id.ToString()));

    public IActionResult OnPostNewBooking(Guid guid, BookingDto newBooking) {
        if (!ModelState.IsValid) {
            return Page(); 
        }
        var (success, message) = _bookings.Create(
            hotelId: guid,
            dateTime: newBooking.Date,
            guestId: newBooking.GuestId,
            roomId: newBooking.RoomId,
            bookingDuration: newBooking.BookingDuration
        );
        if (!success) {
            ModelState.AddModelError("", message!);
            return Page();
        }
        return RedirectToPage();
    }

    public IActionResult OnPostEditBooking(Guid id, Guid bookingId, Dictionary<Guid, BookingDto> editBokings) {
        if (!ModelState.IsValid) { return Page(); }
        var booking = _bookings.FindById(id);
        if (booking is null) {
            return RedirectToPage();
        }
        _mapper.Map(editBokings[bookingId], booking);
        var (success, message) = _bookings.Update(booking);
        if (!success) {
            ModelState.AddModelError("", message!);
            return Page();
        }
        return RedirectToPage();
    }

    public IActionResult OnGet(Guid guid) {
        return Page();
    }
    
    public IActionResult OnPostDelete(Guid guid)
    {
        var booking = _bookings.FindById(guid);
        if (booking is null)
        {
            return RedirectToPage("/Hotels/Index");
        }
        var (success, message) = _bookings.Delete(booking);
        if (!success) { Message = message; }
        return RedirectToPage("/Hotels/Index");
    }
    
    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
        // SELECT * FROM Stores INNER JOIN Offers ON (...)
        // INNER JOIN Product ON (...)
        var hotel = _hotels.Set
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Room)
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Guest)
            .FirstOrDefault(h => h.Id == Guid);
        if (hotel is null) {
            context.Result = RedirectToPage("/Hotels/Index");
            return;
        }
        Hotel = hotel;
        Bookings = hotel.Bookings.ToList();
        BookingsToDelete = hotel.Bookings.ToDictionary(b => b.Id, b => false); // corrected Bookings

      //  EditBokings = bookings.ToDictionary(b => b.Id, b => b);
        EditBokings = _bookings.Set.Where(b => b.Hotel.Id == Guid)
            .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
            .ToDictionary(b => b.Id, b => b);
        
    }
}
