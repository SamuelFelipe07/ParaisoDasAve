/*CREATE TABLE Fornecedores (
    id_forn INT AUTO_INCREMENT PRIMARY KEY,
    nome_forn VARCHAR(255),
    cnpj_forn VARCHAR(20),
    razao_social_forn VARCHAR(255),
    data_criacao_forn DATE,
    telefone_forn VARCHAR(20),
    email_forn VARCHAR(255),
    bairro_forn VARCHAR(100),
    rua_forn VARCHAR(255),
    numero_forn VARCHAR(20),
    cep_forn VARCHAR(20),
    estado_forn VARCHAR(50),
    cidade_forn VARCHAR(50)
);
*/

using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace AppExemplo.Models
{
    public class Fornecedor
    {
        public int id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public string? razaoSocial { get; set; }
        public string? dataCriacao { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? estado { get; set; }
        public string? cidade { get; set; }
        public string? bairro { get; set; }
        public string? rua { get; set; }
        public int? numero { get; set; }
        public string? Cep { get; set; }

        
    }
}

