
using Microsoft.EntityFrameworkCore;
using PhoneDirectory.Domain.Entities;

namespace PhoneDirectory.Infrastructure.Data;

public class PhoneDirectoryContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<User> Users { get; set; }

    public PhoneDirectoryContext(DbContextOptions<PhoneDirectoryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasKey(c => c.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        // Seed an admin user
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = "Admin"
        });
    }
}



//v3
