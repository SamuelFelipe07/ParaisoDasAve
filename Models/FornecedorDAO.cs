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

        // 🔹 INSERIR
        public void Inserir(Fornecedor fornecedor)
        {
            var comando = _conexao.CreateCommand(@"
                INSERT INTO Fornecedores
                (nome_forn, cnpj_forn, razao_social_forn, data_criacao_forn, telefone_forn, email_forn,
                 estado_forn, cidade_forn, bairro_forn, rua_forn, numero_forn, cep_forn)
                VALUES
                (@nome, @cnpj, @razao, @data, @telefone, @email,
                 @estado, @cidade, @bairro, @rua, @numero, @cep)
            ");

            comando.Parameters.AddWithValue("@nome", fornecedor.Nome);
            comando.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
            comando.Parameters.AddWithValue("@razao", fornecedor.razaoSocial);
            comando.Parameters.AddWithValue("@data", fornecedor.dataCriacao);
            comando.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
            comando.Parameters.AddWithValue("@email", fornecedor.Email);
            comando.Parameters.AddWithValue("@estado", fornecedor.estado);
            comando.Parameters.AddWithValue("@cidade", fornecedor.cidade);
            comando.Parameters.AddWithValue("@bairro", fornecedor.bairro);
            comando.Parameters.AddWithValue("@rua", fornecedor.rua);
            comando.Parameters.AddWithValue("@numero", fornecedor.numero);
            comando.Parameters.AddWithValue("@cep", fornecedor.Cep);

            comando.ExecuteNonQuery();
        }

        // 🔹 LISTAR TODOS
        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();

            var comando = _conexao.CreateCommand("SELECT * FROM Fornecedores");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(new Fornecedor
                {
                    id = leitor.GetInt32("id_forn"),
                    Nome = leitor.GetString("nome_forn"),
                    Cnpj = leitor.GetString("cnpj_forn"),
                    razaoSocial = leitor.GetString("razao_social_forn"),
                    dataCriacao = leitor.GetString("data_criacao_forn"),
                    Telefone = leitor.GetString("telefone_forn"),
                    Email = leitor.GetString("email_forn"),
                    estado = leitor.GetString("estado_forn"),
                    cidade = leitor.GetString("cidade_forn"),
                    bairro = leitor.GetString("bairro_forn"),
                    rua = leitor.GetString("rua_forn"),
                    numero = leitor.IsDBNull(leitor.GetOrdinal("numero_forn"))
                        ? null
                        : leitor.GetInt32("numero_forn"),
                    Cep = leitor.GetString("cep_forn")
                });
            }

            return lista;
        }

        // 🔹 BUSCAR POR ID
        public Fornecedor BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand(
                "SELECT * FROM Fornecedores WHERE id_forn = @id"
            );
            comando.Parameters.AddWithValue("@id", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                return new Fornecedor
                {
                    id = leitor.GetInt32("id_forn"),
                    Nome = leitor.GetString("nome_forn"),
                    Cnpj = leitor.GetString("cnpj_forn"),
                    razaoSocial = leitor.GetString("razao_social_forn"),
                    dataCriacao = leitor.GetString("data_criacao_forn"),
                    Telefone = leitor.GetString("telefone_forn"),
                    Email = leitor.GetString("email_forn"),
                    estado = leitor.GetString("estado_forn"),
                    cidade = leitor.GetString("cidade_forn"),
                    bairro = leitor.GetString("bairro_forn"),
                    rua = leitor.GetString("rua_forn"),
                    numero = leitor.IsDBNull(leitor.GetOrdinal("numero_forn"))
                        ? null
                        : leitor.GetInt32("numero_forn"),
                    Cep = leitor.GetString("cep_forn")
                };
            }

            return null;
        }

        // 🔹 ATUALIZAR
        public void Atualizar(Fornecedor fornecedor)
        {
            var comando = _conexao.CreateCommand(@"
                UPDATE Fornecedores SET
                    nome_forn = @nome,
                    cnpj_forn = @cnpj,
                    razao_social_forn = @razao,
                    data_criacao_forn = @data,
                    telefone_forn = @telefone,
                    email_forn = @email,
                    estado_forn = @estado,
                    cidade_forn = @cidade,
                    bairro_forn = @bairro,
                    rua_forn = @rua,
                    numero_forn = @numero,
                    cep_forn = @cep
                WHERE id_forn = @id
            ");

            comando.Parameters.AddWithValue("@id", fornecedor.id);
            comando.Parameters.AddWithValue("@nome", fornecedor.Nome);
            comando.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
            comando.Parameters.AddWithValue("@razao", fornecedor.razaoSocial);
            comando.Parameters.AddWithValue("@data", fornecedor.dataCriacao);
            comando.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
            comando.Parameters.AddWithValue("@email", fornecedor.Email);
            comando.Parameters.AddWithValue("@estado", fornecedor.estado);
            comando.Parameters.AddWithValue("@cidade", fornecedor.cidade);
            comando.Parameters.AddWithValue("@bairro", fornecedor.bairro);
            comando.Parameters.AddWithValue("@rua", fornecedor.rua);
            comando.Parameters.AddWithValue("@numero", fornecedor.numero);
            comando.Parameters.AddWithValue("@cep", fornecedor.Cep);

            comando.ExecuteNonQuery();
        }

        // 🔹 EXCLUIR
        public void Excluir(int id)
        {
            var comando = _conexao.CreateCommand(
                "DELETE FROM Fornecedores WHERE id_forn = @id"
            );
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
        }
    }
}
