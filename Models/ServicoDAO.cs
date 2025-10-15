using AppExemplo.Configs;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AppExemplo.Models
{
    public class ServicoDAO
    {
        private readonly Conexao _conexao;

        public ServicoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // INSERIR
        public void Inserir(Servico servico)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO GestaoServicos (id_fun_fk, descricao_ser, tipo_ser, valor_ser, id_cli_fk)
                    VALUES (@id_fun_fk, @descricao_ser, @tipo_ser, @valor_ser, @id_cli_fk)
                ");

                comando.Parameters.AddWithValue("@id_fun_fk", servico.Id_Funcionario);
                comando.Parameters.AddWithValue("@descricao_ser", servico.Descricao);
                comando.Parameters.AddWithValue("@tipo_ser", servico.Tipo);
                comando.Parameters.AddWithValue("@valor_ser", servico.Valor);
                comando.Parameters.AddWithValue("@id_cli_fk", servico.Id_Clientes);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir serviço: " + ex.Message);
            }
        }

        // LISTAR TODOS (com INNER JOIN)
        public List<Servico> Listar()
        {
            var lista = new List<Servico>();

            var comando = _conexao.CreateCommand(@"
                SELECT 
                    s.id_ser,
                    s.descricao_ser,
                    s.tipo_ser,
                    s.valor_ser,
                    s.id_fun_fk,
                    s.id_cli_fk,
                    f.nome_fun AS NomeFuncionario,
                    c.nome_cli AS NomeCliente
                FROM GestaoServicos s
                INNER JOIN Funcionarios f ON s.id_fun_fk = f.id_fun
                INNER JOIN Cliente c ON s.id_cli_fk = c.id_cli
                ORDER BY s.id_ser DESC
            ");

            using (var leitor = comando.ExecuteReader())
            {
                while (leitor.Read())
                {
                    var servico = new Servico
                    {
                        Id = leitor.GetInt32("id_ser"),
                        Descricao = leitor.IsDBNull(leitor.GetOrdinal("descricao_ser")) ? string.Empty : leitor.GetString("descricao_ser"),
                        Tipo = leitor.IsDBNull(leitor.GetOrdinal("tipo_ser")) ? string.Empty : leitor.GetString("tipo_ser"),
                        Valor = leitor.IsDBNull(leitor.GetOrdinal("valor_ser")) ? string.Empty : leitor.GetDouble("valor_ser").ToString("F2"),
                        Id_Funcionario = leitor.GetInt32("id_fun_fk"),
                        Id_Clientes = leitor.GetInt32("id_cli_fk"),
                        NomeFuncionario = leitor.GetString("NomeFuncionario"),
                        NomeCliente = leitor.GetString("NomeCliente")
                    };

                    lista.Add(servico);
                }
            }

            return lista;
        }
    }
}
