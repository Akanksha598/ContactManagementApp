using ContactManagementAPI.Data;
using ContactManagementAPI.Models;

namespace ContactManagementAPI.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public readonly ApplicationDbContext _db;

        public ContactRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Contact> UpdateAsync(Contact entity)
        {
            _db.Contacts.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
