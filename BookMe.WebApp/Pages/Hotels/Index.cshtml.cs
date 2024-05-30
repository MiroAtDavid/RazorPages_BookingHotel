using BookMe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMe.WebApp.Pages.Hotels;

public class IndexModel : PageModel {

    private readonly HotelRepository _hotels;
    private readonly AuthService _authService;
    
    public IndexModel(HotelRepository hotelRepository, AuthService authService)
    {
        _hotels = hotelRepository;
        _authService = authService;
    }
    
    [TempData] public string? Message { get; set; }
    public IReadOnlyList<HotelRepository.HotelWithBookingCount> Hotels { get; set; } =
        new List<HotelRepository.HotelWithBookingCount>();
    
    public void OnGet() {
        Hotels = _hotels.GetHotelWithBookingCounts();
    }

    // TODO
    // Missing HotelRepository User Manager Entry
    
    public bool CanEditHotel(Guid hotelGuid) =>
        _authService.IsAdmin ||
            Hotels.FirstOrDefault(h => h.Id == hotelGuid)?.Manager?.Username == _authService.Username;
}
 