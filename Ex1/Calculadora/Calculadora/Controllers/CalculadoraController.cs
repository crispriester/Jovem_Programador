using Calculadora.Dto;
using Microsoft.AspNetCore.Mvc;


namespace Calculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : ControllerBase
    {

        private readonly ILogger<CalculadoraController> _logger;

        public CalculadoraController(ILogger<CalculadoraController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostCalculo")]
        public IActionResult Post([FromBody]CalculoDto calculoDto)
        {
            return Ok(TratarTipoCalculo(calculoDto));
        }

        private ResultadoCalculoDto TratarTipoCalculo(CalculoDto calculoDto)
        {
            var metodoEscolhido = new ResultadoCalculoDto { Message = "Você deve escolher algum método (soma, subtração, multiplicar ou dividir)", Value = 0 };

            switch (calculoDto.TipoCalculo.ToLower())
            {
                case "soma":
                    metodoEscolhido = Somar(calculoDto);
                    break;
                case "divisao":
                    metodoEscolhido = Dividir(calculoDto);
                    break;
                case "subtracao":
                    metodoEscolhido = Subtrair(calculoDto);
                    break;
                case "multiplicacao":
                    metodoEscolhido = Multiplicar(calculoDto);
                    break;
            }

            return metodoEscolhido;
        }

        private ResultadoCalculoDto Somar(CalculoDto calculoDto)
        {
            if (calculoDto.ValorA == 0 || calculoDto.ValorB == 0)
            {
                return new ResultadoCalculoDto { Message = "Soma não realizada por conta de algum valor estar zerado", Value = 0 };
            }

            return new ResultadoCalculoDto { Message = "Soma: ", Value = calculoDto.ValorA + calculoDto.ValorB };
        }

        private ResultadoCalculoDto Subtrair(CalculoDto calculoDto)
        {
            return new ResultadoCalculoDto { Message = "Subtração: ", Value = calculoDto.ValorA - calculoDto.ValorB };
        }

        private ResultadoCalculoDto Multiplicar(CalculoDto calculoDto)
        {
            return new ResultadoCalculoDto { Message = "Multiplicação: ", Value = calculoDto.ValorA * calculoDto.ValorB };
        }

        private ResultadoCalculoDto Dividir(CalculoDto calculoDto)
        {
            if (calculoDto.ValorB == 0)
            {
                return new ResultadoCalculoDto { Message = "Divisão não realizada por conta do valor B ser 0", Value = 0 };
            }

            return new ResultadoCalculoDto { Message = "Divisão: ", Value = calculoDto.ValorA / calculoDto.ValorB };
        }
    }
}