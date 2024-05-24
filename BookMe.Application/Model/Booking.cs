using System.ComponentModel.DataAnnotations;

namespace BookMe.Model;

public class Booking : IEntity<Guid>
{
    public Guid Id { get; private set; }
    public DateTime Date { get; set; }
    public Guid GuestId { get;  set; }
    public virtual Guest Guest { get; set; }
    public Guid HotelId { get; set; }
    public virtual Hotel Hotel { get; set; }
    public int RoomId { get; set; }
    public virtual Room Room { get; set; }
    public decimal BookingPrice { get; private set; }
    public int BookingDuration { get; set; }
    
    // Constructor
    public Booking(Hotel hotel, DateTime date, Guest guest, Room room, int bookingDuration) {
        Id = new Guid();
        Hotel = hotel;
        Date = date;
        Guest = guest;
        Room = room;
        BookingDuration = bookingDuration;
        BookingPrice = CalculateBookingPrice();
    }
    
    // Parameterless Constructor
    protected Booking(){}
    
    
    // Other Methods
    // Calculate booking price
    private decimal CalculateBookingPrice() {
        BookingPrice = Room.Price * BookingDuration;
        return BookingPrice;
    }
}