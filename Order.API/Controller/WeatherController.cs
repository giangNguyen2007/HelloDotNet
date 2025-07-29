using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.GRPC;
using SharedLibrary.MassTransit.RabbitMQ;

namespace Order.API.Controller;

[Route("/weather")]
[ApiController]
public class WeatherController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly GrpcClientService _grpcClientService;
    
    private readonly IPublishEndpoint _publishEndpoint;   // MassTransit Publisher

    public WeatherController(GrpcClientService grpcClientService, IPublishEndpoint publishEndpoint)
    {
        _grpcClientService = grpcClientService;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        // send grpc to ProductAPI go get stock
        var response = await _grpcClientService.GetProductAsync(1);
        
        Console.WriteLine($"current product quantity = {response.Stock}");
        
        
        // send back message to update stock
        _publishEndpoint.Publish<UpdateStockMsg>(new UpdateStockMsg()
            {
                ProductId = 1,
                Quantity = response.Stock - 1
            }
        );
        
        return Ok(response);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> PostWeather()
    {
        return Ok("Gng");
    }
}