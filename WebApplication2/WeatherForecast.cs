namespace WebApplication2
{
    [Test]
    public class WeatherForecast
    {
        [RequestTypeAttribute]
        public string Test1(string text)
        {
            return text;
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}