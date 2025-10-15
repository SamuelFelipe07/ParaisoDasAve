using Microsoft.VisualBasic;

namespace AppExemplo.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public string? CEP { get; set; }
        public string? dataNascimento { get; set; }
        public string? estado { get; set; }
        public string? cidade { get; set; }
        public string? bairro { get; set; }
        public string? rua { get; set; }
        public int? numero { get; set; }
        
    }
}
