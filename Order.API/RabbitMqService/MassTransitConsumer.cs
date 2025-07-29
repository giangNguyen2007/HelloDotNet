using MassTransit;
using SharedLibrary;
using SharedLibrary.MassTransit.RabbitMQ;

namespace Order.API.RabbitMqService;

public class MassTransitConsumer : IConsumer<MqMessage>
{
    public async Task Consume(ConsumeContext<MqMessage> context)
    {
        var message = context.Message;
        // Your business logic here
        Console.WriteLine($"Received Message: {message}");
        await Task.CompletedTask;
    }
}

public class MassTransitConsumer2 : IConsumer<IMqRequest>
{
    
    public async Task Consume(ConsumeContext<IMqRequest> context)
    {
        var message = context.Message;
        // Your business logic here
        Console.WriteLine($"Received Message: {message.Id}");
        
        await context.RespondAsync<IMqResponse>(new
        {
            Id = message.Id,
            Message = "Hello from ProductAPI"
        });
        
        await Task.CompletedTask;
    }
}