using Microsoft.VisualBasic;

namespace AppExemplo.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string? Validade { get; set; }
        public int Id_Produto { get; set; }
        public int Id_Fornecedor { get; set; }

        // Dados do INNER JOIN
        public string? NomeProduto { get; set; }
        public string? NomeFornecedor { get; set; }
        public string? Marca { get; set; }
    }
}

