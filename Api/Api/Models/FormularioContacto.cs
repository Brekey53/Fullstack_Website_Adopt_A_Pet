namespace ApiAndreLeonorProjetoFinal.Models
{
    public class FormularioContacto
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? TipoVisita {  get; set; }
        public bool AceitaContacto { get; set; } //podemos apagar, nao foi preciso usar
    }
}
