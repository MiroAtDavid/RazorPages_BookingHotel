using System.ComponentModel.DataAnnotations;

namespace BookMe.Model;

public class Booking {
    public Guid Guid { get; private set; }
    public DateTime Date { get; set; }
    public Guid GuestId { get; private set; }
    public virtual Guest Guest { get; set; }
    public int HotelId { get; set; }
    public virtual Hotel Hotel { get; private set; }
    protected List<Room> _rooms = new();
    public virtual IReadOnlyCollection<Room> Rooms => _rooms;
    public decimal BookingPrice { get; private set; }
    public int BookingDuration { get; private set; }
    
    // Constructor
    public Booking(Hotel hotel, DateTime date, Guest guest, int bookingDuration) {
        Guid = new Guid();
        Hotel = hotel;
        Date = date;
        Guest = guest;
        BookingDuration = bookingDuration;
    }
    
    // Parameterless Constructor
    protected Booking(){}
    
    
    // Other Methods
    // Add room to booking
    public void AddBookingRoom(Room room) {
        _rooms.Add(room);
    }

    // Calculate booking price
    public void CalculateBookingPrice() {
        BookingPrice = _rooms.Sum(room => room.Price) * BookingDuration;
    }
}