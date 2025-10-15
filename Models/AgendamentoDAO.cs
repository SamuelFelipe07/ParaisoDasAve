using AppExemplo.Configs;

namespace AppExemplo.Models
{
    public class AgendamentoDAO
    {

        private readonly Conexao _conexao;

        public AgendamentoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

/*CREATE TABLE Agendamento(
id_age int primary key auto_increment,
data_age varchar(300),
hora_age varchar(300),
id_ser_fk int,
foreign key (id_ser_fk) references GestaoServicos(id_ser)
);
*/
        public void Inserir(Agendamento agendamento)
        {
            try
            {
                var comando = _conexao.CreateCommand("INSERT INTO Agendamento VALUES (null, @_data_age, @_hora_age)");

                comando.Parameters.AddWithValue("@_data_age", agendamento.Data);
                comando.Parameters.AddWithValue("@_hora_age", agendamento.Hora);
                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Agendamento> ListarTodos()
        {
            var lista = new List<Agendamento>();

            var comando = _conexao.CreateCommand("SELECT * FROM Agendamento");
            var leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var agendamento = new Agendamento
                {
                    Id = leitor.GetInt32("id_age"),
                    Data = leitor.GetString("data_age"),
                    Hora = leitor.GetString("hora_age"),
                };

                lista.Add(agendamento);
            }

            return lista;
        }

    }
}