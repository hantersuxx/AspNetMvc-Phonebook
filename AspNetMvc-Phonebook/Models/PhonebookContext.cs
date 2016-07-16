using System.Data.Entity;

namespace AspNetMvc_Phonebook.Models
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext() : base("Phonebook") { }
        public DbSet<Contact> Contacts { get; set; }
    }
}