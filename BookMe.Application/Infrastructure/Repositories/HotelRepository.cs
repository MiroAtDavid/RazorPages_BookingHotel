using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class HotelRepository : Repository<Hotel, Guid>
{
    public record HotelWithBookingCount(
        Guid Id,
        string Name,
        Stars Stars,
        Address Address,
        User? Manager,
        int? BookingsCount);
    
    public HotelRepository(BookingContext db) : base(db) {}

    public IReadOnlyList<HotelWithBookingCount> GetHotelWithBookingCounts() {
        return _db.Hotels
            .Select(h => new HotelWithBookingCount(h.Id, h.Name,h.Stars, h.Address,h.Manager, h.Bookings.Count()))
            .ToList();
    }

    public override (bool success, string message) Delete(Hotel hotel) {
        if (hotel.Bookings.Any()) {
            return (false, $"The Hotel {hotel.Name} has open bookings");
        }
        return base.Delete(hotel);
    }
}
