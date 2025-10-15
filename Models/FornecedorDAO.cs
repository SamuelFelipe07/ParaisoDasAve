using AppExemplo.Configs;
using System;
using System.Collections.Generic;

namespace AppExemplo.Models
{
    public class FornecedorDAO
    {
        private readonly Conexao _conexao;

        public FornecedorDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // 🔹 INSERIR NOVO FORNECEDOR
        public void Inserir(Fornecedor fornecedor)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"INSERT INTO Fornecedores (nome_forn, cnpj_forn, razao_social_forn, data_criacao_forn, telefone_forn, email_forn, estado_forn, cidade_forn, bairro_forn, rua_forn, numero_forn, cep_forn)
                    VALUES (@_nome_forn, @_cnpj_forn, @_razao_social_forn, @_data_criacao_forn, @_telefone_forn, @_email_forn, @_estado_forn, @_cidade_forn, @_bairro_forn, @_rua_forn, @_numero_forn, @_cep_forn)
                ");

                comando.Parameters.AddWithValue("@_nome_forn", fornecedor.Nome);
                comando.Parameters.AddWithValue("@_cnpj_forn", fornecedor.Cnpj);
                comando.Parameters.AddWithValue("@_razao_social_forn", fornecedor.razaoSocial);
                comando.Parameters.AddWithValue("@_data_criacao_forn", fornecedor.dataCriacao);
                comando.Parameters.AddWithValue("@_telefone_forn", fornecedor.Telefone);
                comando.Parameters.AddWithValue("@_email_forn", fornecedor.Email);
                comando.Parameters.AddWithValue("@_estado_forn", fornecedor.estado);
                comando.Parameters.AddWithValue("@_cidade_forn", fornecedor.cidade);
                comando.Parameters.AddWithValue("@_bairro_forn", fornecedor.bairro);
                comando.Parameters.AddWithValue("@_rua_forn", fornecedor.rua);
                comando.Parameters.AddWithValue("@_numero_forn", fornecedor.numero);
                comando.Parameters.AddWithValue("@_cep_forn", fornecedor.Cep);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir fornecedor: " + ex.Message);
            }
        }

        // 🔹 LISTAR TODOS OS FORNECEDORES
        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();

            var comando = _conexao.CreateCommand("SELECT * FROM Fornecedores");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var fornecedor = new Fornecedor
                {
                    id = leitor.GetInt32("id_forn"),
                    Nome = leitor.IsDBNull(leitor.GetOrdinal("nome_forn")) ? "" : leitor.GetString("nome_forn"),
                    Cnpj = leitor.IsDBNull(leitor.GetOrdinal("cnpj_forn")) ? "" : leitor.GetString("cnpj_forn"),
                    razaoSocial = leitor.IsDBNull(leitor.GetOrdinal("razao_social_forn")) ? "" : leitor.GetString("razao_social_forn"),
                    dataCriacao = leitor.IsDBNull(leitor.GetOrdinal("data_criacao_forn")) ? "" : leitor.GetString("data_criacao_forn"),
                    Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_forn")) ? "" : leitor.GetString("telefone_forn"),
                    Email = leitor.IsDBNull(leitor.GetOrdinal("email_forn")) ? "" : leitor.GetString("email_forn"),
                    estado = leitor.IsDBNull(leitor.GetOrdinal("estado_forn")) ? "" : leitor.GetString("estado_forn"),
                    cidade = leitor.IsDBNull(leitor.GetOrdinal("cidade_forn")) ? "" : leitor.GetString("cidade_forn"),
                    bairro = leitor.IsDBNull(leitor.GetOrdinal("bairro_forn")) ? "" : leitor.GetString("bairro_forn"),
                    rua = leitor.IsDBNull(leitor.GetOrdinal("rua_forn")) ? "" : leitor.GetString("rua_forn"),
                    numero = leitor.IsDBNull(leitor.GetOrdinal("numero_forn")) ? (int?)null : leitor.GetInt32("numero_forn"),
                    Cep = leitor.IsDBNull(leitor.GetOrdinal("cep_forn")) ? "" : leitor.GetString("cep_forn")
                };

                lista.Add(fornecedor);
            }

            return lista;
        }
    }
}
