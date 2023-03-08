using Microsoft.Extensions.DependencyInjection;
using NotificationUtils.Services;

namespace NotificationUtils;

public static class ServiceExtension
{
    public static void ConfigureNotificationUtils(this IServiceCollection services)
    {
        services.AddScoped<HttpService>();
        services.AddScoped<SmsService>();
    }
}
