using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using NotificationUtils.Dto;
using NotificationUtils.Models;

namespace NotificationUtils.Services;

public class HttpService
{
    private HttpClient _client = new();
    public async Task SendSparrowSms(SparrowSmsPostDto dto)
    {
        var baseUrl = "http://api.sparrowsms.com/v2/sms/";
        var queryParams = new Dictionary<string, string>
        {
            { "token", dto.Token },
            { "to", dto.Phone },
            { "from", dto.SenderId },
            { "text", dto.Text }
        };
        var queryString = QueryHelpers.AddQueryString("", queryParams);

        var fullUrl = baseUrl + queryString;
        var message = new HttpRequestMessage(HttpMethod.Get, fullUrl);
        
        var responseMessage = await _client.SendAsync(message);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var error = await responseMessage.Content.ReadFromJsonAsync<SparrowErrorModel>() ?? throw new Exception("Something went wrong");
            throw new Exception(error.Response);
        }
    }
}
