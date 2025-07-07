namespace Game.API.AsyncService;

public interface IMassTransitService
{
    Task PublishMessage(string message);
    Task SendRequestForResponseAsync(int id);
}