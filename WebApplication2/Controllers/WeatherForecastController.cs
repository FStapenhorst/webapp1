using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public delegate string TestDelegate(String text);

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        private void CreateDelegate()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.GetCustomAttributes()
                .OfType<TestAttribute>().Any()).ToList();

            foreach (var classType in types)
            {
                List<MethodInfo> methodInfos = classType.GetMethods()
                    .Where(m => m.GetCustomAttributes(false).OfType<RequestTypeAttribute>().Any())
                    .Select(m => m).ToList();

                foreach (var methodInfo in methodInfos)
                {
                    var requestTypeAttribute = methodInfo.GetCustomAttributes(false).OfType<RequestTypeAttribute>().FirstOrDefault();
                    if (requestTypeAttribute is not null)
                    {
                        var myDelegate = methodInfo.CreateDelegate<TestDelegate>();
                    }
                }
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            CreateDelegate();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}