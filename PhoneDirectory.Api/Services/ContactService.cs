using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using System.Collections.Concurrent;

namespace PhoneDirectory.Api.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;
    private readonly ConcurrentQueue<(string Operation, object Data)> _taskQueue;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
        _taskQueue = new ConcurrentQueue<(string, object)>();
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        return await _repository.CreateAsync(contact);
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        await _repository.UpdateAsync(contact);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task DisableAsync(int id)
    {
        await _repository.DisableAsync(id);
    }

    public async Task BulkUploadAsync(List<Contact> contacts)
    {
        foreach (var contact in contacts.Take(200)) // Limit to 200 contacts
        {
            _taskQueue.Enqueue(("Create", contact));
        }
       await  _repository.BulkCreateAsync(contacts);
    }

    public async Task BulkDeleteAsync(List<int> ids)
    {
        foreach (var id in ids.Take(150)) // Limit to 150 contacts
        {
           await DeleteAsync(id);
        }
    }

    public async Task BulkDisableAsync(List<int> ids)
    {
        foreach (var id in ids.Take(100)) // Limit to 100 contacts
        {
           await DisableAsync(id);
        }
        

    }

    public ConcurrentQueue<(string Operation, object Data)> GetTaskQueue()
    {
        return _taskQueue;
    }
}




