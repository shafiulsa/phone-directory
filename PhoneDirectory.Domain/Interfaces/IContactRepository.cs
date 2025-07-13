
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Domain.Interfaces;

public interface IContactRepository
{
    Task<Contact> CreateAsync(Contact contact);
    Task<Contact?> GetByIdAsync(int id);
    Task<List<Contact>> GetAllAsync();
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task DisableAsync(int id);
    Task<List<Contact>> BulkCreateAsync(List<Contact> contacts);
}