using NotificationUtils.Entities;

namespace NotificationUtils.Repository;

public interface ISmsRepository
{
    Task<SparrowSmsEntry> InsertAsync(SparrowSmsEntry sparrowSms);
}
