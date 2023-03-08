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
        
        var tx = TransactionScopeHelper.Create();
        try
        {
            await _httpService.SendSparrowSms(dto);
            await _smsRepository.InsertAsync(sms);
        }
        catch (Exception e)
        {
            tx.Dispose();
            Console.WriteLine(e);
            throw;
        }

        
        return sms;
    }
}
