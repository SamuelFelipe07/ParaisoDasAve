using AppExemplo.Configs;

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
                var comando = _conexao.CreateCommand(
                    "INSERT INTO Estoque VALUES (null, @_quantidade, @_validade, @_id_produto, @_id_fornecedor);"
                );

                comando.Parameters.AddWithValue("@_quantidade", estoque.Quantidade);
                comando.Parameters.AddWithValue("@_validade", estoque.Validade);
                comando.Parameters.AddWithValue("@_id_produto", estoque.Id_Produto);
                comando.Parameters.AddWithValue("@_id_fornecedor", estoque.Id_Fornecedor);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public List<Estoque> ListarTodos()
        {
            var lista = new List<Estoque>();

            var comando = _conexao.CreateCommand(@"
                SELECT 
                    e.id_est,
                    e.quantidade_est,
                    e.validade_est,
                    p.id_pro AS produto_id,
                    p.nome_pro AS produto_nome,
                    f.id_forn AS fornecedor_id,
                    f.nome_forn AS fornecedor_nome
                FROM estoque e
                INNER JOIN produto p ON p.id_pro = e.id_pro_fk
                INNER JOIN fornecedores f ON f.id_forn = e.id_forn_fk;
            ");

            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var estoque = new Estoque
                {
                    Id = leitor.GetInt32("id_est"),
                    Quantidade = leitor.GetInt32("quantidade_est"),
                    Validade = leitor.IsDBNull(leitor.GetOrdinal("validade_est"))
                        ? ""
                        : leitor.GetString("validade_est"),

                    Id_Produto = leitor.GetInt32("produto_id"),
                    NomeProduto = leitor.GetString("produto_nome"),

                    Id_Fornecedor = leitor.GetInt32("fornecedor_id"),
                    NomeFornecedor = leitor.GetString("fornecedor_nome")
                };

                lista.Add(estoque);
            }

            return lista;
        }


        // ✅ BUSCAR POR ID
        public Estoque? BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand(
                "SELECT * FROM Estoque WHERE id_est = @id;"
            );

            comando.Parameters.AddWithValue("@id", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                var estoque = new Estoque
                {
                    Id = leitor.GetInt32("id_est"),
                    Quantidade = leitor.GetInt32("quantidade_est"),
                    Validade = leitor.IsDBNull(leitor.GetOrdinal("validade_est"))
                        ? ""
                        : leitor.GetString("validade_est"),
                    Id_Produto = leitor.GetInt32("id_pro_fk"),
                    Id_Fornecedor = leitor.GetInt32("id_forn_fk")
                };

                return estoque;
            }
            else
            {
                return null;
            }
        }

        // ✅ ATUALIZAR
        public void Atualizar(Estoque estoque)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    "UPDATE Estoque SET quantidade_est = @_quantidade, validade_est = @_validade, id_pro_fk = @_id_produto, id_forn_fk = @_id_fornecedor WHERE id_est = @_id;"
                );

                comando.Parameters.AddWithValue("@_quantidade", estoque.Quantidade);
                comando.Parameters.AddWithValue("@_validade", estoque.Validade);
                comando.Parameters.AddWithValue("@_id_produto", estoque.Id_Produto);
                comando.Parameters.AddWithValue("@_id_fornecedor", estoque.Id_Fornecedor);
                comando.Parameters.AddWithValue("@_id", estoque.Id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        // ✅ EXCLUIR
        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    "DELETE FROM Estoque WHERE id_est = @id;"
                );

                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
}
