using AppExemplo.Configs;
using System;
using System.Collections.Generic;

namespace AppExemplo.Models
{
    public class ProdutoDAO
    {
        private readonly Conexao _conexao;

        public ProdutoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // 🔹 INSERIR
        public void Inserir(Produto produto)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO produto 
                    VALUES (null, @_nome_pro, @_descricao_pro, @_quantidade_pro, @_marca_pro, @_preco_pro)
                ");

                comando.Parameters.AddWithValue("@_nome_pro", produto.Nome);
                comando.Parameters.AddWithValue("@_descricao_pro", produto.Descricao);
                comando.Parameters.AddWithValue("@_quantidade_pro", produto.Quantidade);
                comando.Parameters.AddWithValue("@_marca_pro", produto.Marca);
                comando.Parameters.AddWithValue("@_preco_pro", produto.Preco);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir produto: " + ex.Message);
            }
        }

        // 🔹 LISTAR TODOS
        public List<Produto> ListarTodos()
        {
            var lista = new List<Produto>();

            var comando = _conexao.CreateCommand("SELECT * FROM produto;");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var produto = new Produto
                {
                    Id = leitor.GetInt32("id_pro"),
                    Nome = leitor.GetString("nome_pro"),
                    Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao_pro")) ? "" : leitor.GetString("descricao_pro"),
                    Quantidade = leitor.GetInt32("quantidade_pro"),
                    Marca = leitor.IsDBNull(leitor.GetOrdinal("marca_pro")) ? "" : leitor.GetString("marca_pro"),
                    Preco = leitor.GetFloat("preco_pro")
                };

                lista.Add(produto);
            }

            return lista;
        }

        // 🔹 BUSCAR POR ID
        public Produto BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand(
                "SELECT * FROM produto WHERE id_pro = @_id_pro;"
            );

            comando.Parameters.AddWithValue("@_id_pro", id);
            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                return new Produto
                {
                    Id = leitor.GetInt32("id_pro"),
                    Nome = leitor.GetString("nome_pro"),
                    Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao_pro")) ? "" : leitor.GetString("descricao_pro"),
                    Quantidade = leitor.GetInt32("quantidade_pro"),
                    Marca = leitor.IsDBNull(leitor.GetOrdinal("marca_pro")) ? "" : leitor.GetString("marca_pro"),
                    Preco = leitor.GetFloat("preco_pro")
                };
            }

            return null!;
        }

        // 🔹 ATUALIZAR
        public void Atualizar(Produto produto)
        {
            var comando = _conexao.CreateCommand(@"
                UPDATE produto SET
                    nome_pro = @_nome_pro,
                    descricao_pro = @_descricao_pro,
                    quantidade_pro = @_quantidade_pro,
                    marca_pro = @_marca_pro,
                    preco_pro = @_preco_pro
                WHERE id_pro = @_id_pro;
            ");

            comando.Parameters.AddWithValue("@_id_pro", produto.Id);
            comando.Parameters.AddWithValue("@_nome_pro", produto.Nome);
            comando.Parameters.AddWithValue("@_descricao_pro", produto.Descricao);
            comando.Parameters.AddWithValue("@_quantidade_pro", produto.Quantidade);
            comando.Parameters.AddWithValue("@_marca_pro", produto.Marca);
            comando.Parameters.AddWithValue("@_preco_pro", produto.Preco);

            comando.ExecuteNonQuery();
        }

        // 🔹 EXCLUIR
        public void Excluir(int id)
        {
            var comando = _conexao.CreateCommand(
                "DELETE FROM produto WHERE id_pro = @_id_pro;"
            );

            comando.Parameters.AddWithValue("@_id_pro", id);
            comando.ExecuteNonQuery();
        }
    }
}
