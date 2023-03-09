using System.Text.Json;
using System.Text.Json.Serialization;
using NotificationUtils.Dto;
using NotificationUtils.Entities;
using NotificationUtils.Helpers;
using NotificationUtils.Repository;

namespace NotificationUtils.Services;

public class SmsService
{
    private readonly ISmsRepository _smsRepository;
    private readonly HttpService _httpService;

    public SmsService(ISmsRepository smsRepository, HttpService httpService)
    {
        _smsRepository = smsRepository;
        _httpService = httpService;
    }

    public async Task<SparrowSmsEntry> PostSms(SparrowSmsPostDto dto)
    {
        var sms = new SparrowSmsEntry
        {
            SenderId = dto.SenderId,
            Phone = dto.Phone,
            Text = dto.Text
        };
        
        using var tx = TransactionScopeHelper.Create();
        try
        {
            await _httpService.SendSparrowSms(dto);
            Console.WriteLine(JsonSerializer.Serialize(dto));
            await _smsRepository.InsertAsync(sms);
            tx.Complete();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            tx.Dispose();
            throw;
        }

        
        return sms;
    }
}
