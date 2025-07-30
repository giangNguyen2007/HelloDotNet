using MassTransit;
using SharedLibrary.MassTransit.RabbitMQ;

namespace ProductAPI.AsyncService;

public class UpdateStockConsumerService : IConsumer<UpdateStockMsg>
{
    public async Task Consume(ConsumeContext<UpdateStockMsg> context)
    {
        var message = context.Message;
        // Your business logic here
        Console.WriteLine($"Update stock request: product id = {message.ProductId}, change quantity = {message.Quantity}");
        await Task.CompletedTask;
    }
}