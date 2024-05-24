using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class GuestRepository(BookingContext db) : Repository<Guest, Guid>(db);
