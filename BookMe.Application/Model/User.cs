using System.ComponentModel.DataAnnotations;

namespace BookMe.Model;

public enum Usertype { Owner = 1, Admin }

public class User : IEntity<Guid> {
    
    public Guid Id { get; private set; }
    [MaxLength(255)]
    public string Username { get; set; }
    [MaxLength(44)]  // 256 bit Hash as base64
    public string Salt { get; set; }
    [MaxLength(88)]  // 512 bit SHA512 Hash as base64
    public string PasswordHash { get; set; }
    public Usertype Usertype { get; set; }
    public ICollection<Hotel> Hotels { get; } = new List<Hotel>();
    
    public User(string username, string salt, string passwordHash, Usertype usertype) {
        Id = Guid.NewGuid();
        Username = username; 
        Salt = salt;
        PasswordHash = passwordHash;
        Usertype = usertype; 
        }

    protected User() { }
        
}
