using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookMe.WebApp.Pages.Bookings {
    public class IndexModel : PageModel
    {
        /*
        public record HotelsWithBookingCount(
            Guid Guid,
            string Name,
            Stars Stars,
            Address Address
        );

        private readonly BookingContext _db;
        public List<HotelsWithBookingCount> Hotels { get; private set; } = new();
        public IndexModel(BookingContext db) {
            _db = db;
        }
        */
        public void OnGet()
        {

        }
    }
}