```mermaid

classDiagram 
    
Guest o--|> Address
Hotel o--|> Address
Employee o--|> Address
Hotel o--|> Stars
Hotel o--|> BookingConfirmation
Hotel *--|> Room
Booking *--|> Room
Booking *--|> Hotel
Booking *--|> Guest
Hotel *--|> Employee
Employee <|-- Manager
Employee <|-- Receptionist
Room <|-- Deluxe
Room <|-- Suit
Deluxe o--|> SpecialService


    class Address {
        <<value object>>
        +Street: string
        +City: string
        +Zip: string
    }

    class Booking {
        <<Aggregate>>
        +Id: Guid
        +Date: DateTime
        +GuestId: int
        +Guest: Guest
        +HotelId: int
        +Hotel: Hotel
        #_rooms: List~Room~
        +Rooms: IReadOnlyCollection~Room~
        +BookingPrice: decimal
        +BookingDuration: int
        +AddBookingRoom(Room)
        +CalculateBookinPrice()
    }    
     
    class Guest {
        <<Aggregate>>
        +Id: int
        +Firstname: string
        +Lastname: string
        +Address: Address
        #_bookings: List~Booking~
        +Bookings: IReadOnlyCollection~Booking~
        +AddBooking(Booking)
    }

    class Employee {
        <<abstract>>
        +Id: int
        +Firstname: string  
        +Lastname: string
        +Salary: decimal
        +Address: Address
        +Hotel: Hotel
        +PerformDuties()*
    }

    class Manager{
        +Vehicle: string
        +Phone: string
    }

    class Receptionist {
        +Email: string
    }

    class Hotel {
        +Id: int
        +Name: string
        +Stars: Stars
        +Address: Address
        #_hotelRooms: List~Room~
        +HotelRooms: IReadOnlyCollection~Room~
        #_hotelEmployees: List~Employees~
        +HotelEmployees: IReadOnlyCollection~Employee~
        #_bookings: List~Booking~
        +Bookings: IReadOnlyCollection~Booking~
        +AddHotelRoom(Room)  
        +AddHotelEmployee(Employee) 
        -ValidateBooking(Booking):bool 
        +AddBooking(Booking)
        +CreateBookingConfirmation(Booking):BookingConfirmation
        -GenerateConfirmationCode(): string
    }

    class Stars {
        <<enum>>
        Zero
        One
        Two
        Three
        Four
        Five
    }

    class Room {
        +Id: int
        +Price: decimal
        +HotelId: int
        +Hotel: Hotel
    }

    class Deluxe {
        #_specialServices List~SpecialService~
        +SpecialServices: IReadOnlyCollection~SpecialService~
        +AddSpecialService(): 
        +CalculateSpecialServicePrice(): decimal
    }

    class Suit {
        +Jacuzzi: bool
        +AddJacuzziPrice(): bool

    }

    class BookingConfirmation {
        +Booking: Booking
        +Confirmationcode: string
    }


    class SpecialService {
        +Name: string
        +Price: decimal
    }


````