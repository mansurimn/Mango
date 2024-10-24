using Mango.Services.Web.Models;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mango.Web.Service
{
    public class Baseservice : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Baseservice(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                HttpRequestMessage message = new();// HtttpRequest();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        System.Text.Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiresponse = null;
                switch (requestDto.ApiType)
                {
                    case Utility.SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case Utility.SD.ApiType.POST:
                        message.Method = HttpMethod.Post;

                        break;
                    case Utility.SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utility.SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;

                        break;
                    default:
                        break;
                }

                apiresponse = await client.SendAsync(message);

                switch (apiresponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new ResponseDto { IsSucess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSucess = false, Message = "Access denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSucess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSucess = false, Message = "Internal Server Error" };
                    default:
                        var apicontent = await apiresponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apicontent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {

                var dto = new ResponseDto { 
                Message=ex.Message,
                IsSucess=false
                };
                return dto;
            }


        }
    }
}
