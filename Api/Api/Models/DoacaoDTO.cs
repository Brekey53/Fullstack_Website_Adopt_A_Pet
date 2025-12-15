namespace ApiAndreLeonorProjetoFinal.Models
{
    public class DoacaoDTO
    {
        public string? NomeDoador { get; set; }
        public string? TipoDoacao { get; set; }
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
