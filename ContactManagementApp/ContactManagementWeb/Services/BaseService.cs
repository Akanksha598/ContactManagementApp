using ContactManagementUtility;
using ContactManagementWeb.Models;
using ContactManagementWeb.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ContactManagementWeb.Services;

public class BaseService : IBaseService
{
    public APIResponse responseModel { get; set; }

    public IHttpClientFactory httpClient { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        responseModel = new();
        this.httpClient = httpClient;
    }

    public async Task<T> SendAsync<T>(APIRequest apiRequest)
    {
        try
        {
            var client = httpClient.CreateClient("ContactAPI");

            HttpRequestMessage message = new HttpRequestMessage();

            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);

            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                                                    Encoding.UTF8,
                                                    "application/json");
            }

            message.Method = apiRequest.ApiType switch
            {
                Constants.ApiType.POST => HttpMethod.Post,
                Constants.ApiType.PUT => HttpMethod.Put,
                Constants.ApiType.DELETE => HttpMethod.Delete,
                Constants.ApiType.GET => HttpMethod.Get,
                _ => HttpMethod.Get
            };

            HttpResponseMessage apiResponse = null;

            apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();

            try
            {
                APIResponse apiResponseMessage = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                switch (apiResponseMessage?.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        apiResponseMessage.StatusCode = HttpStatusCode.BadRequest;
                        apiResponseMessage.IsSuccessStatusCode = false;
                        var res = JsonConvert.SerializeObject(apiResponseMessage, Formatting.Indented);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);

                        return returnObj;

                    case HttpStatusCode.NotFound:
                        apiResponseMessage.StatusCode = HttpStatusCode.NotFound;
                        apiResponseMessage.IsSuccessStatusCode = false;
                        var resNotFound = JsonConvert.SerializeObject(apiResponseMessage, Formatting.Indented);
                        var returnObjNotFound = JsonConvert.DeserializeObject<T>(resNotFound);

                        return returnObjNotFound;
                }
            }
            catch (Exception)
            {

                var apiResponseMessage = JsonConvert.DeserializeObject<T>(apiContent);


                return apiResponseMessage;

            }

            var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);

            return APIResponse;
        }
        catch (Exception e)
        {

            var dto = new APIResponse
            {
                ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                IsSuccessStatusCode = false
            };

            var res = JsonConvert.SerializeObject(dto, Formatting.Indented);
            var apiResponseMessage = JsonConvert.DeserializeObject<T>(res);

            return apiResponseMessage;
        }

    }
}
