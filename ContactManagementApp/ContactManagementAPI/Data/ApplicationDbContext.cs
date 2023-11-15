using ContactManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
