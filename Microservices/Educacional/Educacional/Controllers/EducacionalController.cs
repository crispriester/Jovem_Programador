using Microsoft.AspNetCore.Mvc;
using Educacional.Dto;

namespace Educacional.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EducacionalController : ControllerBase
    {
        [HttpPost(Name = "Post")]
        public IActionResult Post([FromBody]InputEducacionalDto inputEducacional)
        {
            var retornoDto = new RetornoDto
            {
                DataGeracao = inputEducacional.DataGeracao.AddHours(-3),
                RetornoMaterias = new List<RetornoMateriaDto>()
            };

            inputEducacional.Materias.ForEach(x => retornoDto.RetornoMaterias.Add(RetornarObjeto(x)));

            //foreach (var materia in inputEducacional.RetornoMaterias)
            //{
            //    var notaFinal = CalcularMedia(materia.Notas);
                
            //    var retornoMateriaDto = new RetornoMateriaDto
            //    {
            //        Nome = materia.Nome,
            //        Professor = materia.Professor,
            //        Nota = new RetornoNotaDto
            //        {
            //            Nota = notaFinal
            //        },
            //        Situacao = VerificarSituacao(notaFinal)
            //    };

            //    retornoDto.RetornoMaterias.Add(retornoMateriaDto);
            //}

            return Ok(retornoDto);
        }

        private string VerificarSituacao(double notaFinal) => notaFinal >= 7 ? "Aprovado!" : "Reprovado!";
        //{
        //    if (notaFinal >= 7)
        //    {
        //        return "Aprovado!";
        //    }
        //    return "Reprovado!";
        //}

        private double CalcularMedia(List<double> notas) => notas.Average();
        //{
        //    double soma = 0d;

        //    foreach (var nota in notas)
        //    {
        //        soma += nota;
        //    }

        //    return soma / notas.Count;
        //}

        private RetornoMateriaDto RetornarObjeto(MateriaDto materiaDto)
        {
            var notaFinal = CalcularMedia(materiaDto.Notas);

            var retornoMateriaDto = new RetornoMateriaDto
            {
                Nome = materiaDto.Nome,
                Professor = materiaDto.Professor,
                Nota = new RetornoNotaDto
                {
                    Nota = notaFinal
                },
                Situacao = VerificarSituacao(notaFinal)
            };

            return retornoMateriaDto;
        }
    }
}