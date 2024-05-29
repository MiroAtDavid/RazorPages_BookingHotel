using AutoMapper;
using BookMe.Infrastructure.Repositories;
using BookMe.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookMe.WebApp.Pages.Hotels;
[Authorize]
public class Details : PageModel {

    private readonly AuthService _authService;
    private readonly HotelRepository _hotels;
    public Details(IMapper mapper, HotelRepository hotels, AuthService authService) {
        _hotels = hotels;
        _authService = authService;
    }
    
    [FromRoute]
    public Guid Guid { get; set; }
    public Hotel? Hotel { get; set; }

    public IActionResult OnGet(Guid guid) {
        return Page();
    }

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
        var hotel = _hotels.Set
            .Include(h => h.Manager)
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Room)
            .Include(h => h.Bookings)
            .ThenInclude(b => b.Guest)
            .FirstOrDefault(h => h.Id == Guid);
        if (hotel is null) {
            context.Result = RedirectToPage("/Stores/Index");
            return;
        }

        var username = _authService.Username;
        if (!_authService.HasRole("Admin") && username != hotel.Manager?.Username)
        {
            context.Result = new ForbidResult();
        }
        Hotel = hotel;
    }

}