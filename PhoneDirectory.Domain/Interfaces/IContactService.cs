using PhoneDirectory.Domain.Entities;
using System.Collections.Concurrent;

namespace PhoneDirectory.Domain.Interfaces;

public interface IContactService
{
    Task<Contact> CreateAsync(Contact contact);
    Task<Contact?> GetByIdAsync(int id);
    Task<List<Contact>> GetAllAsync();
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task DisableAsync(int id);
    Task BulkUploadAsync(List<Contact> contacts);
    Task BulkDeleteAsync(List<int> ids);
    Task BulkDisableAsync(List<int> ids);
    ConcurrentQueue<(string Operation, object Data)> GetTaskQueue(); // Added method
}


// using PhoneDirectory.Domain.Entities;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace PhoneDirectory.Domain.Interfaces;

// public interface IContactService
// {
//     Task<Contact> CreateAsync(Contact contact);
//     Task<Contact> GetByIdAsync(int id);
//     Task<List<Contact>> GetAllAsync();
//     Task UpdateAsync(Contact contact);
//     Task DeleteAsync(int id);
//     Task DisableAsync(int id);
//     Task BulkUploadAsync(List<Contact> contacts);
//     Task BulkDeleteAsync(List<int> ids);
//     Task BulkDisableAsync(List<int> ids);
// }