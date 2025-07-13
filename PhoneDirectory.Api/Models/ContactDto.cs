
namespace PhoneDirectory.Api.Models;

public class ContactDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Balance { get; set; }
    public string Address { get; set; }
    public string Group { get; set; }
    public bool Status { get; set; }
}