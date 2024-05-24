using Bogus;
using BookMe.Model;
using Microsoft.EntityFrameworkCore;
namespace BookMe.Infrastructure;

public class BookingContext : DbContext {
    
    public BookingContext(DbContextOptions opt) : base(opt) { }
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Address> Addresses => Set<Address>();

    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        // Hotel
        modelBuilder.Entity<Hotel>().HasKey(Hotel => Hotel.Guid); 
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.HotelRooms)
            .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId)
            .IsRequired();
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.HotelEmlopyees)
            .WithOne(e => e.Hotel)
            .HasForeignKey(e => e.HotelId)
            .IsRequired();
        modelBuilder.Entity<Hotel>()
            .HasMany(h => h.Bookings)
            .WithOne(b => b.Hotel)
            .HasForeignKey(b => b.HotelId)
            .IsRequired();
        
        // Room
        modelBuilder.Entity<Room>().HasDiscriminator(h => h.RoomType);

        // Employee
        modelBuilder.Entity<Employee>().HasKey(employee => employee.Guid); 
        modelBuilder.Entity<Employee>().HasDiscriminator(h => h.Role);
            
        // Guest
        modelBuilder.Entity<Guest>().HasKey(guest => guest.Guid); 
        modelBuilder.Entity<Guest>()
                .HasMany(g => g.Bookings)
                .WithOne(b => b.Guest)
                .HasForeignKey(b => b.GuestId)
                .IsRequired();
            
        // Booking
        modelBuilder.Entity<Booking>().HasKey(booking => booking.Guid); 
        
        // Address
        modelBuilder.Entity<Address>().HasKey(address => address.Id);

    }

    // Creating Test Data with Bogus Faker
    public void Seed() {
        
        // Random sed num for database
        Randomizer.Seed = new Random(1358);

        // Address data with faker
        var addresses = new Faker<Address>("de").CustomInstantiator(f => new Address(
            street: f.Address.StreetAddress(),
            zip: f.Address.ZipCode(),
            city: f.Address.City()
        ))
        .Generate(100)
        .ToList();
        Addresses.AddRange(addresses);
        SaveChanges();

        // Guests data with faker
        var guests = new Faker<Guest>("de").CustomInstantiator(f => new Guest(
                firstName: f.Name.FirstName(),
                lastName: f.Name.LastName(),
                email: f.Internet.Email(),
                address: f.Random.ListItem(addresses)
            ))
            .Generate(100)
            .ToList();
        Guests.AddRange(guests);
        SaveChanges();
        
        // Hotel data with faker
        var hotels = new Faker<Hotel>("de").CustomInstantiator(f => new Hotel(
                name: f.Company.CompanyName(),
                stars: f.PickRandom<Stars>(),
                address: f.Random.ListItem(addresses)
            ))
            .Generate(100)
            .ToList();
        Hotels.AddRange(hotels);
        SaveChanges();
        
        // Employee data with faker
        var employees = new Faker<Employee>("de").CustomInstantiator(f => new Employee(
                firstName: f.Name.FirstName(),
                lastName: f.Name.LastName(),
                salary: f.Finance.Amount(2500, 3500),
                address: f.Random.ListItem(addresses),
                hotel: f.Random.ListItem(hotels)
            ))
            .Generate(100)
            .ToList();
        Employees.AddRange(employees);
        SaveChanges();
        
        // Room data with faker
        var rooms = new Faker<Room>("de").CustomInstantiator(f => new Room(
                price: f.Finance.Amount(50, 500),
                hotel: f.Random.ListItem(hotels),
                roomType: f.PickRandom<string>(new[] { "Single", "Double", "Suite" })
            ))
            .Generate(100)
            .ToList();
        Rooms.AddRange(rooms);
        SaveChanges();
        
        // Bookings data with faker
        var bookings = new Faker<Booking>("de").CustomInstantiator(f => new Booking(
                hotel: f.Random.ListItem(hotels),
                date: new DateTime(2024, 6, 1).AddDays(f.Random.Int(0, 4 * 30)),
                bookingDuration: f.Random.Int(1, 14),
                guest: f.Random.ListItem(guests),
                room: f.Random.ListItem(rooms)
            ))
            .Generate(100)
            .ToList();
        Bookings.AddRange(bookings);
        SaveChanges();

    }
}