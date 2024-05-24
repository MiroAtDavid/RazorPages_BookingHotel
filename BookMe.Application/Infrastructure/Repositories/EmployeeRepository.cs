using BookMe.Model;

namespace BookMe.Infrastructure.Repositories;

public class EmployeeRepository(BookingContext db) : Repository<Employee, Guid>(db);