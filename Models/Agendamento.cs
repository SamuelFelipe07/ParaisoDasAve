/*CREATE TABLE Agendamento(
id_age int primary key auto_increment,
data_age varchar(300),
hora_age varchar(300),
id_ser_fk int,
foreign key (id_ser_fk) references GestaoServicos(id_ser)
);
*/

using Microsoft.VisualBasic;

namespace AppExemplo.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string? Data { get; set; }
        public string? Hora { get; set; }
        public int Id_Servico { get; set; }

    }
}