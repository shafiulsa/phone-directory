
namespace PhoneDirectory.Domain.Entities;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public bool Status { get; set; } = true; // true = active, false = disabled
}




// namespace PhoneDirectory.Domain.Entities;

// public class Contact
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public string Email { get; set; }
//     public string PhoneNumber { get; set; }
//     public decimal Balance { get; set; }
//     public string Address { get; set; }
//     public string Group { get; set; }
//     public bool Status { get; set; }
// }