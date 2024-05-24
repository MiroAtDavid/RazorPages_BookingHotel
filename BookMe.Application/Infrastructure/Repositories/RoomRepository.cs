using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class RoomRepository(BookingContext db) : Repository<Room, int>(db);
