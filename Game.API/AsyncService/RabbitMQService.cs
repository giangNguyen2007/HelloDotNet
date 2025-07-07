using System.Text;
using RabbitMQ.Client;

namespace Game.API.AsyncService;

public class RabbitMQService
{
    public static async Task PublishAsync()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(
            queue: "gngQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: new Dictionary<string, object>
            {
                { "x-queue-type", "classic" }
            }!
        );
        
        string message = "Hello from async RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "gngQueue",
            mandatory: false,
            // basicProperties: null,
            body: body,
            cancellationToken: CancellationToken.None
        );
        
        Console.WriteLine($" [x] Sent message = {message} to queue gngQueue");
    }
    
}