using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class AddressRepository(BookingContext db) : Repository<Address, int>(db);