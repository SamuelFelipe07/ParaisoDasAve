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


    }
}