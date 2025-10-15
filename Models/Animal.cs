using Microsoft.AspNetCore.SignalR;

namespace AppExemplo.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Raca { get; set; }
        public int Idade { get; set; }
        public string? Especie { get; set; }
        public string? Porte { get; set; }
        public float Peso { get; set; }
        public string? Diagnostico { get; set; }
        public string? Sexo { get; set; }
        public int? id_cli_fk { get; set; }

        public string NomeCliente { get; set; }
    }
}