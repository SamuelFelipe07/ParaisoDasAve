using AppExemplo.Configs;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AppExemplo.Models
{
    public class EstoqueDAO
    {
        private readonly Conexao _conexao;

        public EstoqueDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // ✅ INSERIR
        public void Inserir(Estoque estoque)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO Estoque (quantidade_est, validade_est, id_pro_fk, id_forn_fk)
                    VALUES (@quantidade_est, @validade_est, @id_pro_fk, @id_forn_fk)
                ");

                comando.Parameters.AddWithValue("@quantidade_est", estoque.Quantidade);
                comando.Parameters.AddWithValue("@validade_est", estoque.Validade);
                comando.Parameters.AddWithValue("@id_pro_fk", estoque.Id_Produto);
                comando.Parameters.AddWithValue("@id_forn_fk", estoque.Id_Fornecedor);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir estoque: " + ex.Message);
            }
        }

        // ✅ LISTAR TODOS (com INNER JOIN)
        public List<Estoque> Listar()
        {
            var lista = new List<Estoque>();

            var comando = _conexao.CreateCommand(@"
                SELECT 
                    e.id_est,
                    e.quantidade_est,
                    e.validade_est,
                    e.id_pro_fk,
                    e.id_forn_fk,
                    p.nome_pro AS NomeProduto,
                    p.marca_pro AS Marca,
                    f.nome_forn AS NomeFornecedor
                FROM Estoque e
                INNER JOIN Produto p ON e.id_pro_fk = p.id_pro
                INNER JOIN Fornecedores f ON e.id_forn_fk = f.id_forn
                ORDER BY p.nome_pro
            ");

            using (var leitor = comando.ExecuteReader())
            {
                while (leitor.Read())
                {
                    var estoque = new Estoque
                    {
                        Id = leitor.GetInt32("id_est"),
                        Quantidade = leitor.GetInt32("quantidade_est"),
                        Validade = leitor.IsDBNull(leitor.GetOrdinal("validade_est"))
                            ? string.Empty
                            : leitor.GetString("validade_est"),
                        Id_Produto = leitor.GetInt32("id_pro_fk"),
                        Id_Fornecedor = leitor.GetInt32("id_forn_fk"),
                        NomeProduto = leitor.GetString("NomeProduto"),
                        NomeFornecedor = leitor.GetString("NomeFornecedor"),
                        Marca = leitor.GetString("Marca")
                    };

                    lista.Add(estoque);
                }
            }

            return lista;
        }

        // ✅ ATUALIZAR
        public void Atualizar(Estoque estoque)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    UPDATE Estoque 
                    SET 
                        quantidade_est = @quantidade_est,
                        validade_est = @validade_est,
                        id_pro_fk = @id_pro_fk,
                        id_forn_fk = @id_forn_fk
                    WHERE id_est = @id_est
                ");

                comando.Parameters.AddWithValue("@id_est", estoque.Id);
                comando.Parameters.AddWithValue("@quantidade_est", estoque.Quantidade);
                comando.Parameters.AddWithValue("@validade_est", estoque.Validade);
                comando.Parameters.AddWithValue("@id_pro_fk", estoque.Id_Produto);
                comando.Parameters.AddWithValue("@id_forn_fk", estoque.Id_Fornecedor);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar estoque: " + ex.Message);
            }
        }

        // ✅ EXCLUIR
        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand("DELETE FROM Estoque WHERE id_est = @id_est");
                comando.Parameters.AddWithValue("@id_est", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir estoque: " + ex.Message);
            }
        }
    }
}
