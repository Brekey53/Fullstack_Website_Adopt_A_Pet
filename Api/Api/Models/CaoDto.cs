namespace ApiAndreLeonorProjetoFinal.Models
{
    public class CaoDto
    {
        public int CaoId { get; set; }
        public string Nome { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Porte { get; set; }
        public string Sexo { get; set; }
        public bool Castrado { get; set; }
        public bool? Disponivel { get; set; }
        public string Caracteristica { get; set; }
        public string Foto { get; set; }
    }
}
