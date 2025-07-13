using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Domain.Entities;
using PhoneDirectory.Domain.Interfaces;
using PhoneDirectory.Infrastructure.Data;

namespace PhoneDirectory.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly PhoneDirectoryContext _context;

    public ContactRepository(PhoneDirectoryContext context)
    {
        _context = context;
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task<List<Contact>> BulkCreateAsync(List<Contact> contacts)
    {
        await _context.Contacts.AddRangeAsync(contacts);
        await _context.SaveChangesAsync();
        return contacts;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        IQueryable<Contact> contacts =  _context.Contacts.Where(item=> item.Status );
        return contacts.ToList();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DisableAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            contact.Status = false;
            await _context.SaveChangesAsync();
        }
    }
}