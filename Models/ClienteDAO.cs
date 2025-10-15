using AppExemplo.Configs;
using Microsoft.AspNetCore.Http.HttpResults;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppExemplo.Models
{
    public class ClienteDAO
    {
        private readonly Conexao _conexao;

        public ClienteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }
        public void Inserir(Cliente cliente)
        {
            try
            {

                var comando = _conexao.CreateCommand("INSERT INTO Cliente VALUES (null, @_nome_cli, @_cpf_cli, @_data_nascimento_cli, @_rg_cli, @_bairro_cli, @_rua_cli, @_numero_cli, @_cep_cli , @_estado_cli, @_cidade_cli)");

                comando.Parameters.AddWithValue("@_id_cli", cliente.id);
                comando.Parameters.AddWithValue("@_nome_cli", cliente.Nome);
                comando.Parameters.AddWithValue("@_cpf_cli", cliente.CPF);
                comando.Parameters.AddWithValue("@_data_nascimento_cli", cliente.dataNascimento);
                comando.Parameters.AddWithValue("@_rg_cli", cliente.RG);
                comando.Parameters.AddWithValue("@_bairro_cli", cliente.bairro);
                comando.Parameters.AddWithValue("@_rua_cli", cliente.rua);
                comando.Parameters.AddWithValue("@_numero_cli", cliente.numero);
                comando.Parameters.AddWithValue("@_cep_cli", cliente.CEP);
                comando.Parameters.AddWithValue("@_estado_cli", cliente.estado);
                comando.Parameters.AddWithValue("@_cidade_cli", cliente.cidade);
                

                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Cliente> ListarTodos()
        {
            var lista = new List<Cliente>();

            var comando = _conexao.CreateCommand("SELECT * FROM Cliente");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {

                var cliente = new Cliente
                {
                    id = leitor.GetInt32("id_cli"),
                    Nome = leitor.GetString("nome_cli"),
                    CPF = leitor.IsDBNull(leitor.GetOrdinal("cpf_cli")) ? "" : leitor.GetString("cpf_cli"),
                    dataNascimento = leitor.IsDBNull(leitor.GetOrdinal("data_nascimento_cli")) ? "" : leitor.GetString("data_nascimento_cli"),
                    RG = leitor.IsDBNull(leitor.GetOrdinal("rg_cli")) ? "" : leitor.GetString("rg_cli"),
                    bairro = leitor.IsDBNull(leitor.GetOrdinal("bairro_cli")) ? "" : leitor.GetString("bairro_cli"),
                    rua = leitor.IsDBNull(leitor.GetOrdinal("rua_cli")) ? "" : leitor.GetString("rua_cli"),

                            numero = leitor.IsDBNull(leitor.GetOrdinal("numero_cli"))
                                ? (int?)null
                                : leitor.GetInt32(leitor.GetOrdinal("numero_cli")),
 
                    CEP = leitor.IsDBNull(leitor.GetOrdinal("cep_cli")) ? "" : leitor.GetString("cep_cli"),
                    estado = leitor.IsDBNull(leitor.GetOrdinal("estado_cli")) ? "" : leitor.GetString("estado_cli"),
                    cidade = leitor.IsDBNull(leitor.GetOrdinal("cidade_cli")) ? "" : leitor.GetString("cidade_cli")

                };

                lista.Add(cliente);
            }

            return lista;
        }

    }
}
    
