using RabbitMQ.Client;

namespace Product.API.RabbitMqService;

public class RabbitMqListenerService
{
    public static async Task CosummerAsync()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "gngQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }
}