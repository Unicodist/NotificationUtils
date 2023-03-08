using System.Transactions;

namespace NotificationUtils.Helpers;

public class TransactionScopeHelper
{
    public static TransactionScope Create() => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
}
