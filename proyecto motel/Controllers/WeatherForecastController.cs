using Microsoft.AspNetCore.Mvc;

[Route("WeatherForecastWeb")]
public class WeatherForecastController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecasts = Enumerable.Range(1, 5).Select(index => new proyecto_motel.WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToList();

        return View(forecasts);
    }
}
