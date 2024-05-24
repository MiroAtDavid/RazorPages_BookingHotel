namespace BookMe.Model;

public class Employee {
    public Guid Guid { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; private set; }
    public Guid HotelId { get; private set; }
    public virtual Hotel Hotel { get; private set; }
    public string Role { get; protected set; }

    // Construcotor
    public Employee(string firstName, string lastName, decimal salary, Address address, Hotel hotel) {
        Guid = new Guid();
        FirstName = firstName;
        LastName = lastName;
        Salary = salary;
        Address = address;
        Hotel = hotel;
    }
    
    // Parameterless constructor
    protected Employee(){}
    
    // Common methods for all employees
    public bool PerformDuties() {
        return true;
    }
}