using ContactManagementAPI.Models;

namespace ContactManagementAPI.Repository;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact> UpdateAsync(Contact contact);
}
