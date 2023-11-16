using ContactManagementWeb.Models;

namespace ContactManagementWeb.Services.IServices;

public interface IContactService
{
    Task<T> GetAllAsync<T>();

    Task<T> GetAsync<T>(int id);

    Task<T> CreateAsync<T>(ContactCreateDTO dto);

    Task<T> DeleteAsync<T>(int id);

    Task<T> UpdateAsync<T>(ContactUpdateDTO dto);
}
