using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace NotificationUtils.Services;

public class AwsSesService
{
    private readonly IAmazonSesMeta _meta;

    public AwsSesService(IAmazonSesMeta meta)
    {
        _meta = meta;
    }

    public async Task SendEmailAsync(IEnumerable<string> to, string subject, string body)
    {
        var credentials = new BasicAWSCredentials(_meta.Key, _meta.Pass);
        using var client = new AmazonSimpleEmailServiceClient(credentials, Amazon.RegionEndpoint.APSouth1);
        var request = new SendEmailRequest
        {
            Source = _meta.Email,
            Destination = new Destination
            {
                ToAddresses = to.ToList()
            },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body
                {
                    Html = new Content(body)
                }
            }
        };

        try
        {
            var response = await client.SendEmailAsync(request);
            return;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            client.Dispose();
        }
    }
}
