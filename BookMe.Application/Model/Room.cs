namespace BookMe.Model;

public class Room {
    
    public int Id { get; private set; }
    public decimal Price { get; set; }
    public int HotelId { get; private set; }
    public Hotel Hotel { get; private set; }
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