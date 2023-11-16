using ContactManagementUtility;
using ContactManagementWeb.Models;
using ContactManagementWeb.Services.IServices;

namespace ContactManagementWeb.Services;

public class ContactService : BaseService, IContactService
{
    private readonly IHttpClientFactory _clientFactory;

    private string contactApiUrl;
    public ContactService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
    {
        _clientFactory = httpClient;
        contactApiUrl = configuration.GetValue<string>("ServiceUrls:ContactAPI");
    }

    public Task<T> CreateAsync<T>(ContactCreateDTO dto)
    {
        return SendAsync<T>(new APIRequest
        {
            ApiType = Constants.ApiType.POST,
            Url = contactApiUrl + Constants.ContactApiUrl,
            Data = dto
        });
    }

    public Task<T> DeleteAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest
        {
            ApiType = Constants.ApiType.DELETE,
            Url = contactApiUrl + Constants.ContactApiUrl + id
        });
    }

    public Task<T> GetAllAsync<T>()
    {
        return SendAsync<T>(new APIRequest
        {
            ApiType = Constants.ApiType.GET,
            Url = contactApiUrl + Constants.ContactApiUrl
        });
    }

    public Task<T> GetAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest
        {
            ApiType = Constants.ApiType.GET,
            Url = contactApiUrl + Constants.ContactApiUrl + id
        });
    }

    public Task<T> UpdateAsync<T>(ContactUpdateDTO dto)
    {
        return SendAsync<T>(new APIRequest
        {
            ApiType = Constants.ApiType.PUT,
            Url = contactApiUrl + Constants.ContactApiUrl + dto.ID,
            Data = dto
        });
    }
}
