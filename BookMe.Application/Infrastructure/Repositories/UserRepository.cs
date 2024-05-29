using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class UserRepository : Repository<User, Guid>
{
    private readonly ICryptService _cryptService;
    public UserRepository(BookingContext db, ICryptService cryptService) : base(db)
    {
        _cryptService = cryptService;
    }
}