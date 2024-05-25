using BookMe.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace BookMe.WebApp.Pages.Hotels;

public class IndexModel : PageModel {

    private readonly HotelRepository _hotels;
    
    public IndexModel(HotelRepository hotelRepository)
    {
        _hotels = hotelRepository;
    }
    
    [TempData] public string? Message { get; set; }
    public IReadOnlyList<HotelRepository.HotelWithBookingCount> Hotels { get; set; } =
        new List<HotelRepository.HotelWithBookingCount>();
    
    public void OnGet() {
        Hotels = _hotels.GetHotelWithBookingCounts();
    }
}
