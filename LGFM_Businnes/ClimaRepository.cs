using LGFM_Abstraction;
using LGFM_Entities;

namespace LGFM_Businnes
{
    public class ClimaRepository : IClima
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public Task<ICollection<Clima>> GetAll()
        {
            var climas = Enumerable
                .Range(1, 5) // Corregir el uso de Range
                .Select(index => new Clima
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList(); 

            return Task.FromResult((ICollection<Clima>)climas);
        }
    }
}
