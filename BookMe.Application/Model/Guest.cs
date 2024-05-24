namespace BookMe.Model;

public class Guest {
    public Guid Guid { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; private set; }
    public string Email { get; set; }
    
    protected List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    
    // Constructor 
    public Guest(string firstName, string lastName, string email, Address address) {
        Guid = new Guid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }
    
    // Parameterless Constructor
    protected Guest() {}
    
    // Other Methods
    // Add booking to guest
    public void AddBooking(Booking booking) {
        _bookings.Add(booking);
    }
}