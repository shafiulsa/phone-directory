using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Infrastructure.FileHandlers;

public class TextFileHandler
{
    public List<Contact> ParseText(Stream stream)
    {
        var contacts = new List<Contact>();
        using var reader = new StreamReader(stream);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var parts = line.Split(',');
            if (parts.Length >= 6)
            {
                contacts.Add(new Contact
                {
                    Name = parts[0].Trim(),
                    Email = parts[1].Trim(),
                    PhoneNumber = parts[2].Trim(),
                    Balance = decimal.TryParse(parts[3].Trim(), out var balance) ? balance : 0,
                    Address = parts[4].Trim(),
                    Group = parts[5].Trim(),
                    Status = true
                });
            }
        }
        return contacts;
    }
}