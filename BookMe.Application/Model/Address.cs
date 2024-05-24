using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookMe.Model;

// this is an address record
// helps us implement quickly an immutable address with propperties
public class Address{
    public int Id { get; private set; }
    public string Street { get; set; }
    public string Zip { get; set; }
    public string City { get; set; }

    public Address(string street, string zip, string city) {
        Street = street;
        Zip = zip;
        City = city;
    }
    
    protected Address(){}
    
    }
    
