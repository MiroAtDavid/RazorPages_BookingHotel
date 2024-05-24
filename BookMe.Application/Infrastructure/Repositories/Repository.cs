using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public abstract class Repository
{
    // Injecting Database
    protected readonly BookingContext _db;

    // Constructor for database Injection
    protected Repository(BookingContext db) {
        _db = db;
    }

    public Hotel FindByGuid(Guid guid) => _db.Set<Hotel>().FirstOrDefault(hotel => hotel.Guid == guid);


}