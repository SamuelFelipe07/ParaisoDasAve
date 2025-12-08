using AppExemplo.Configs;
using System.Collections.Generic;

namespace AppExemplo.Models
{
    public class FuncionarioDAO
    {
        private readonly Conexao _conexao;

        public FuncionarioDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Funcionario funcionario)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    "INSERT INTO funcionarios VALUES (null, @_nome_fun, @_cpf_fun, @_rg_fun,@_data_nascimento_fun, @_telefone_fun, @_email_fun, @_bairro_fun, @_rua_fun, @_numero_fun, @_sexo_fun, @_conta_bancaria_fun)"
                );

                comando.Parameters.AddWithValue("@_id_fun", funcionario.Id);
                comando.Parameters.AddWithValue("@_nome_fun", funcionario.Nome);
                comando.Parameters.AddWithValue("@_cpf_fun", funcionario.Cpf);
                comando.Parameters.AddWithValue("@_rg_fun", funcionario.Rg);
                comando.Parameters.AddWithValue("@_data_nascimento_fun", funcionario.DataNascimento);
                comando.Parameters.AddWithValue("@_telefone_fun", funcionario.Telefone);
                comando.Parameters.AddWithValue("@_email_fun", funcionario.Email);
                comando.Parameters.AddWithValue("@_bairro_fun", funcionario.Bairro);
                comando.Parameters.AddWithValue("@_rua_fun", funcionario.Rua);
                comando.Parameters.AddWithValue("@_numero_fun", funcionario.Numero);
                comando.Parameters.AddWithValue("@_conta_bancaria_fun", funcionario.ContaBancaria);
                comando.Parameters.AddWithValue("@_sexo_fun", funcionario.Sexo);

                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Funcionario> ListarTodos()
        {
            var lista = new List<Funcionario>();

            var comando = _conexao.CreateCommand("SELECT * FROM funcionarios");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var funcionario = new Funcionario
                {
                    Id = leitor.IsDBNull(leitor.GetOrdinal("id_fun")) ? 0 : leitor.GetInt32(leitor.GetOrdinal("id_fun")),
                    Nome = leitor.IsDBNull(leitor.GetOrdinal("nome_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("nome_fun")),
                    Cpf = leitor.IsDBNull(leitor.GetOrdinal("cpf_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("cpf_fun")),
                    Rg = leitor.IsDBNull(leitor.GetOrdinal("rg_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("rg_fun")),
                    DataNascimento = leitor.IsDBNull(leitor.GetOrdinal("data_nascimento_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("data_nascimento_fun")),
                    Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("telefone_fun")),
                    Email = leitor.IsDBNull(leitor.GetOrdinal("email_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("email_fun")),
                    Bairro = leitor.IsDBNull(leitor.GetOrdinal("bairro_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("bairro_fun")),
                    Rua = leitor.IsDBNull(leitor.GetOrdinal("rua_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("rua_fun")),
                    Numero = leitor.IsDBNull(leitor.GetOrdinal("numero_fun")) ? 0 : leitor.GetInt32(leitor.GetOrdinal("numero_fun")),
                    ContaBancaria = leitor.IsDBNull(leitor.GetOrdinal("conta_bancaria_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("conta_bancaria_fun")),
                    Sexo = leitor.IsDBNull(leitor.GetOrdinal("sexo_fun")) ? string.Empty : leitor.GetString(leitor.GetOrdinal("sexo_fun"))
                };

                lista.Add(funcionario);
            }

            return lista;
        }

        public Funcionario BuscarPorId(int id)
        {
            Funcionario funcionario = null;

            var comando = _conexao.CreateCommand(
                "SELECT * FROM funcionarios WHERE id_fun = @_id_fun"
            );

            comando.Parameters.AddWithValue("@_id_fun", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                funcionario = new Funcionario
                {
                    Id = leitor.GetInt32(leitor.GetOrdinal("id_fun")),
                    Nome = leitor.GetString(leitor.GetOrdinal("nome_fun")),
                    Cpf = leitor.GetString(leitor.GetOrdinal("cpf_fun")),
                    Rg = leitor.GetString(leitor.GetOrdinal("rg_fun")),
                    DataNascimento = leitor.GetString(leitor.GetOrdinal("data_nascimento_fun")),
                    Telefone = leitor.GetString(leitor.GetOrdinal("telefone_fun")),
                    Email = leitor.GetString(leitor.GetOrdinal("email_fun")),
                    Bairro = leitor.GetString(leitor.GetOrdinal("bairro_fun")),
                    Rua = leitor.GetString(leitor.GetOrdinal("rua_fun")),
                    Numero = leitor.GetInt32(leitor.GetOrdinal("numero_fun")),
                    ContaBancaria = leitor.GetString(leitor.GetOrdinal("conta_bancaria_fun")),
                    Sexo = leitor.GetString(leitor.GetOrdinal("sexo_fun"))
                };
            }

            return funcionario;
        }

        public void Atualizar(Funcionario funcionario)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    @"UPDATE funcionarios SET
                        nome_fun = @_nome_fun,
                        cpf_fun = @_cpf_fun,
                        rg_fun = @_rg_fun,
                        data_nascimento_fun = @_data_nascimento_fun,
                        telefone_fun = @_telefone_fun,
                        email_fun = @_email_fun,
                        bairro_fun = @_bairro_fun,
                        rua_fun = @_rua_fun,
                        numero_fun = @_numero_fun,
                        sexo_fun = @_sexo_fun,
                        conta_bancaria_fun = @_conta_bancaria_fun
                      WHERE id_fun = @_id_fun"
                );

                comando.Parameters.AddWithValue("@_id_fun", funcionario.Id);
                comando.Parameters.AddWithValue("@_nome_fun", funcionario.Nome);
                comando.Parameters.AddWithValue("@_cpf_fun", funcionario.Cpf);
                comando.Parameters.AddWithValue("@_rg_fun", funcionario.Rg);
                comando.Parameters.AddWithValue("@_data_nascimento_fun", funcionario.DataNascimento);
                comando.Parameters.AddWithValue("@_telefone_fun", funcionario.Telefone);
                comando.Parameters.AddWithValue("@_email_fun", funcionario.Email);
                comando.Parameters.AddWithValue("@_bairro_fun", funcionario.Bairro);
                comando.Parameters.AddWithValue("@_rua_fun", funcionario.Rua);
                comando.Parameters.AddWithValue("@_numero_fun", funcionario.Numero);
                comando.Parameters.AddWithValue("@_sexo_fun", funcionario.Sexo);
                comando.Parameters.AddWithValue("@_conta_bancaria_fun", funcionario.ContaBancaria);

                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                    "DELETE FROM funcionarios WHERE id_fun = @_id_fun"
                );

                comando.Parameters.AddWithValue("@_id_fun", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
