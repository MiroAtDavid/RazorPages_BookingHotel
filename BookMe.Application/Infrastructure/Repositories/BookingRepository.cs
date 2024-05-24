using System.Runtime.InteropServices.JavaScript;
using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class BookingRepository : Repository<Booking, Guid> {
    public BookingRepository(BookingContext db) : base(db) {}

    public override (bool success, string message) Create(Booking booking) {
        return base.Create(booking);
    }

    public (bool success, string message) 
        Create(DateTime dateTime, Guid guestId, Guid hotelId, int roomId, int bookingDuration)
    {
        var guest = _db.Guests.FirstOrDefault(g => g.Id == guestId);
        if (guest is null)
            return (false, "Guest doesn't exist.");
        var hotel = _db.Hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel is null)
            return (false, "Hotel does not exist.");
        var room = _db.Rooms.FirstOrDefault(r => r.Id == roomId);
        if (room is null)
            return (false, "Room does not exist.");
        
        return base.Create(new Booking(
            hotel: hotel,
            date: dateTime,
            guest: guest,
            room: room,
            bookingDuration : bookingDuration
        ));
    }
}