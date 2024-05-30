using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookMe.Dto;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BookMe.WebApp.Pages.Hotels;

[Authorize(Roles = "Admin, Owner")]
public class Edit : PageModel {
    private readonly HotelRepository _hotels;
    private readonly BookingRepository _bookings;
    private readonly IMapper _mapper;

    public Edit(IMapper mapper, HotelRepository hotels, BookingRepository bookings) {
        _mapper = mapper;
        _hotels = hotels;
        _bookings = bookings;
    }

    [FromRoute] 
    public Guid Guid { get; set; }

    public Hotel? Hotel { get; set; }
    public IReadOnlyList<Booking> Bookings { get; private set; } = new List<Booking>();
    public Dictionary<Guid, BookingDto> EditBokings { get; set; } = new();
    public Dictionary<Guid, bool> BookingsToDelete { get; set; } = new();
    
    public IActionResult OnPostEditBooking(Guid guid, Guid bookingId, Dictionary<Guid, BookingDto> editBokings) {
        if (!ModelState.IsValid) 
            return Page();
        
        var booking = _bookings.FindById(bookingId);
        if (booking == null) 
            return RedirectToPage("/Hotels/Index");
        

        // Get the updated booking DTO
        var updatedBookingDto = editBokings[bookingId];
        // Update only the BookingDuration property in the existing booking entity
        booking.BookingDuration = updatedBookingDto.BookingDuration;
        // Update the date property
        booking.Date = updatedBookingDto.Date; 
        // Calculate the final price again
        booking.CalculateBookingPrice(); 
        var (success, message) = _bookings.Update(booking);
        if (!success) {
            ModelState.AddModelError("", message!);
            return Page();
        }
        return RedirectToPage("/Hotels/Edit");
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