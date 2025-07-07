using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controller;

[Route("/weather")]
[ApiController]
public class WeatherController : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray(); 
        return Ok("Gng");
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