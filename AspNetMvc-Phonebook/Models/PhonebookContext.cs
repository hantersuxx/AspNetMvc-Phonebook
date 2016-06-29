using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNetMvc_Phonebook.Models
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext() : base("PhonebookContext") { }
        public DbSet<Contact> Contacts { get; set; }
    }
}