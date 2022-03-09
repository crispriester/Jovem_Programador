using Newtonsoft.Json;

namespace Calculadora.Dto
{
    public class CalculoDto
    {
        [JsonProperty("tipoCalculo")]
        public string TipoCalculo { get; set; }

        [JsonProperty("valorA")]
        public decimal ValorA { get; set; }

        [JsonProperty("valorB")]
        public decimal ValorB { get; set; }


    }
}
