namespace Educacional.Dto
{
    public class RetornoMateriaDto
    {
        public string Nome { get; set; }

        public string Professor { get; set; }

        public string Situacao { get; set; }

        public RetornoNotaDto Nota { get; set; }
    }
}
