using BookMe.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace BookMe.Test;

[Collection("Sequential")]
public class BookingContextTest {

    private BookingContext GetDatabase(bool deleteDb = false) {
        var db = new BookingContext(new DbContextOptionsBuilder()
            .UseSqlite("Data Source=hotels.db")
            .Options);
        if (deleteDb) {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        return db;
    }
    
    [Fact]
    public void CreateDatabaseSuccessTest() {
        using var db = GetDatabase(deleteDb: true);
    }

    [Fact]
    public void SeedDatabaseTest() {
        using var db = GetDatabase(deleteDb: true);
        db.Seed();
        // Multiple assert statements should be avoided in real unit tests, but in this case
        // the database is tested, not the program logic.
        Assert.True(db.Rooms.Count() == 400);
        Assert.True(db.Addresses.Count() == 100);
        Assert.True(db.Hotels.Count() == 20);
        Assert.True(db.Employees.Count() == 100);
        Assert.True(db.Guests.Count() == 400);
        Assert.True(db.Bookings.Count() == 300);
    }
}