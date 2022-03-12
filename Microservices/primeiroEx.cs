using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//A PORTA NESSE CASO FICARIA NA PASTA PROPERTIES SE FOSSE NO VISUAL STUDIO 2019: 
//EM "profiles" : {"WebApplication": {"applicationUrl"}}

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Sumarios = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Wars", "Salay"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpsGet(Name = "WeatherForecastController")]
        public string Get([FromBody]PessoaDto dto)
        {
            return $"nome: {dto.Nome} Idade: {dto.Idade} Endereço: {dto.Endereco.Cep} Status: {CalcularIdade(dto.Idade)}";
        }

        public string CalcularIdade(int idade) => idade >= 21 ? "Mais velho que o prof" : "mais novo que o prof";
    }

    //Outro arquivo
    public class PessoaDto
    {
        [JsonProperty("nome")]
        public string? Nome { get; set; }

        [JsonProperty("idade")]
        public int Idade { get; set; }

        [JsonProperty("endereco")]
        public PessoaEnderecoDto? Endereco { get; set; } = new PessoaEnderecoDto();
    }

    //Outro arquivo
    public class PessoaEnderecoDto
    {
        [JsonProperty("cep")]
        public int Cep { get; set; }
    }
}