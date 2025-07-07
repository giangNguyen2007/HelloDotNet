using MassTransit;
using SharedLibrary;
using SharedLibrary.MassTransit.RabbitMQ;

namespace Game.API.AsyncService;

public class MassTransitService : IMassTransitService
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    private readonly IRequestClient<IMqRequest> _client;

    public MassTransitService(IPublishEndpoint publishEndpoint, IRequestClient<IMqRequest> client)
    {
        _publishEndpoint = publishEndpoint;
        _client = client;
    }

    public async Task PublishMessage(string message)
    {
        await _publishEndpoint.Publish(new MqMessage()
        {
            Message = message,
            Price = 1
        });
        
        Console.WriteLine($"Published Message: {message}");
    }
    
    public async Task SendRequestForResponseAsync(int id)
    {
        var response = await _client.GetResponse<IMqResponse>(new { Id = id });
        
        Console.WriteLine($"send to RabbitMq: {id}");
        Console.WriteLine($"get response from RabbitMq: {response.Message.Id} {response.Message.Message}");
    }
    
}