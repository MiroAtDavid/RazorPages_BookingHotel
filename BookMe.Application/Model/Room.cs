namespace BookMe.Model;

public class Room {
    
    public int Id { get; private set; }
    public decimal Price { get; set; }
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public string RoomType { get; set; }
    
    // Constructor with properties
    public Room(Hotel hotel, decimal price, string roomType) {
        Hotel = hotel;
        Price = price;
        RoomType = roomType;
    }
    
    // Parameterless Constructor
    protected Room () {}
    
    
    
}