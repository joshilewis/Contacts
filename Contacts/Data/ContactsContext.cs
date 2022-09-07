using Contacts.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ContactNumber> ContactNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
