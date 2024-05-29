using System.Runtime.InteropServices;

namespace BookMe.Model;

public class Hotel : IEntity<Guid> {
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public Stars Stars { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; private set; }
    public Guid? ManagerId { get; set; }
    public User? Manager { get; set; }
    protected List<Room> _hotelRooms = new();
    public virtual IReadOnlyCollection<Room> HotelRooms => _hotelRooms;
    protected List<Employee> _hotelEmployees = new();
    public virtual IReadOnlyCollection<Employee> HotelEmlopyees => _hotelEmployees;
    protected List<Booking> _bookings = new();
    public virtual IReadOnlyCollection<Booking> Bookings => _bookings;
    public Random _random = new Random();
    
    // Constructor 
    public Hotel(string name, Stars stars, Address address, User? manager = null) {
        Id = new Guid();
        Name = name;
        Stars = stars;
        Address = address;
        Manager = manager;
    }
    
    // Parameterless construcotr
    protected Hotel () {}
    
    // Other Methods
    // Add Rooms to Hotel
    public void AddHotelRoom(Room room) {
        _hotelRooms.Add(room);
    }
    
    // Add Employees to Hotel
    public void AddHotelEmployee(Employee employee) {
        _hotelEmployees.Add(employee);
    }

    // Make sure booking date is in future - helper method
    private bool ValidateBooking(Booking booking) {
        return booking.Date > DateTime.Now;
    }

    // Add Booking to Hotel if booking date is in future
    public bool AddBooking(Booking booking) {
        bool isValidBooking = ValidateBooking(booking);
        if (isValidBooking) 
            _bookings.Add(booking);
        return isValidBooking;
    }

    // Create a new booking confirmation record
    public BookingConfirmation CreateBookingConfirmation(Booking booking) {
        bool isValidBooking = ValidateBooking(booking);
        if (isValidBooking) {
            string confirmationCode = GenerateConfirmationCode();
            return new BookingConfirmation { Booking = booking, ConfirmationCode = confirmationCode };
        }
        return null;
    }

    // Generate a confirmation code needed for the BookingConfirmation Record - helper method
    private string GenerateConfirmationCode() {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(characters, 8)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }


}