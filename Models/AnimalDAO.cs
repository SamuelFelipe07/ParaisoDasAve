using AppExemplo.Configs;

namespace AppExemplo.Models
{
    public class AnimalDAO
    {

        private readonly Conexao _conexao;

        public AnimalDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Animal animal)
        {
            try
            {
                var comando = _conexao.CreateCommand("INSERT INTO Animal VALUES (null, @_nome_ani, @_raca_ani, @_idade_ani, @_especie_ani, @_porte_ani, @_peso_ani, @_diagnostico_ani, @_sexo_ani, @_id_cli_fk)");

                comando.Parameters.AddWithValue("@_nome_ani", animal.Nome);
                comando.Parameters.AddWithValue("@_raca_ani", animal.Raca);
                comando.Parameters.AddWithValue("@_idade_ani", animal.Idade);
                comando.Parameters.AddWithValue("@_especie_ani", animal.Especie);
                comando.Parameters.AddWithValue("@_porte_ani", animal.Porte);
                comando.Parameters.AddWithValue("@_peso_ani", animal.Peso);
                comando.Parameters.AddWithValue("@_diagnostico_ani", animal.Diagnostico);
                comando.Parameters.AddWithValue("@_sexo_ani", animal.Sexo);
                comando.Parameters.AddWithValue("@_id_cli_fk", animal.id_cli_fk);

                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Animal> ListarTodos()
        {
            var lista = new List<Animal>();

            var comando = _conexao.CreateCommand(@"
                SELECT 
                    a.id_ani AS Id,
                    a.nome_ani AS Nome,
                    a.raca_ani AS Raca,
                    a.porte_ani AS Porte,
                    a.idade_ani AS Idade,
                    a.especie_ani As Especie,
                    c.nome_cli AS NomeCliente
                FROM Animal a
                INNER JOIN Cliente c ON a.id_cli_fk = c.id_cli
                ORDER BY a.nome_ani
            ");

            using (var leitor = comando.ExecuteReader())
            {
                while (leitor.Read())
                {
                    var animal = new Animal
                    {
                        Id = leitor.GetInt32("Id"),
                        Nome = leitor.IsDBNull(leitor.GetOrdinal("Nome")) ? "" : leitor.GetString("Nome"),
                        Raca = leitor.IsDBNull(leitor.GetOrdinal("Raca")) ? "" : leitor.GetString("Raca"),
                        Idade = leitor.IsDBNull(leitor.GetOrdinal("Idade")) ? 0 : leitor.GetInt32("Idade"),
                        Especie = leitor.IsDBNull(leitor.GetOrdinal("Especie")) ? "" : leitor.GetString("Especie"),
                        Porte = leitor.IsDBNull(leitor.GetOrdinal("Porte")) ? "" : leitor.GetString("Porte"),
                        NomeCliente = leitor.IsDBNull(leitor.GetOrdinal("NomeCliente")) ? "" : leitor.GetString("NomeCliente"),
                    };

                    lista.Add(animal);
                }


            }

            return lista;
        }

        public void Atualizar(Animal animal)
        {
            try
            {
                var comando = _conexao.CreateCommand(
                @"UPDATE Animal 
                SET 
                    nome_ani = @_nome_ani,
                    raca_ani = @_raca_ani,
                    idade_ani = @_idade_ani,
                    especie_ani = @_especie_ani,
                    porte_ani = @_porte_ani,
                    peso_ani = @_peso_ani,
                    diagnostico_ani = @_diagnostico_ani,
                    sexo_ani = @_sexo_ani
                WHERE id_ani = @_id_ani;");

                comando.Parameters.AddWithValue("@_nome_ani", animal.Nome ?? "");
                comando.Parameters.AddWithValue("@_raca_ani", animal.Raca ?? "");
                comando.Parameters.AddWithValue("@_idade_ani", animal.Idade);
                comando.Parameters.AddWithValue("@_especie_ani", animal.Especie ?? "");
                comando.Parameters.AddWithValue("@_porte_ani", animal.Porte ?? "");
                comando.Parameters.AddWithValue("@_peso_ani", animal.Peso);
                comando.Parameters.AddWithValue("@_diagnostico_ani", animal.Diagnostico ?? "");
                comando.Parameters.AddWithValue("@_sexo_ani", animal.Sexo ?? "");
                comando.Parameters.AddWithValue("@_id_ani", animal.Id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Excluir(int id)
        {
            try
            {
                var comando = _conexao.CreateCommand("DELETE FROM Animal WHERE id_ani = @id_ani;");
                comando.Parameters.AddWithValue("@id_ani", id);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public Animal? BuscarPorId(int id)
        {
            var comando = _conexao.CreateCommand("SELECT * FROM Animal WHERE id_ani = @Id;");
            comando.Parameters.AddWithValue("@Id", id);

            var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                var animal = new Animal
                {
                    Id = leitor.GetInt32(leitor.GetOrdinal("id_ani")),
                    Nome = leitor.IsDBNull(leitor.GetOrdinal("nome_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("nome_ani")),
                    Raca = leitor.IsDBNull(leitor.GetOrdinal("raca_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("raca_ani")),
                    Idade = leitor.IsDBNull(leitor.GetOrdinal("idade_ani")) ? 0 : leitor.GetInt32(leitor.GetOrdinal("idade_ani")),
                    Especie = leitor.IsDBNull(leitor.GetOrdinal("especie_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("especie_ani")),
                    Porte = leitor.IsDBNull(leitor.GetOrdinal("porte_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("porte_ani")),
                    Peso = leitor.IsDBNull(leitor.GetOrdinal("peso_ani")) ? 0 : leitor.GetFloat(leitor.GetOrdinal("peso_ani")),
                    Diagnostico = leitor.IsDBNull(leitor.GetOrdinal("diagnostico_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("diagnostico_ani")),
                    Sexo = leitor.IsDBNull(leitor.GetOrdinal("sexo_ani")) ? "" : leitor.GetString(leitor.GetOrdinal("sexo_ani"))
                };

                return animal;
            }
            else
            {
                return null;
            }
        }





    }
}