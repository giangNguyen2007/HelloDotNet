using Microsoft.AspNetCore.Mvc;
using Order.API.GRPC;

namespace Order.API.Controller;

[Route("/weather")]
[ApiController]
public class WeatherController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly GrpcClientService _grpcClientService;

    public WeatherController(GrpcClientService grpcClientService)
    {
        _grpcClientService = grpcClientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var response = await _grpcClientService.GetProductAsync(1);
        
        return Ok(response);
    }
    
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostWeather()
    {
        return Ok("Gng");
    }
}