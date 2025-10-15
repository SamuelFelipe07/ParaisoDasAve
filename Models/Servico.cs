/*CREATE TABLE GestaoServicos (
    id_ser INT AUTO_INCREMENT PRIMARY KEY,
    id_fun_fk INT,
    descricao_ser TEXT,
    tipo_ser VARCHAR(100),
    valor_ser DECIMAL(10,2),
    tempo_ser TIME,
    data_ser DATE,
    hora_ser TIME,
    id_cli_fk INT,
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionarios(id_fun),
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente(id_cli)
);
*/

namespace AppExemplo.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

public class Servico
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public string? Tipo { get; set; }

    public string? Valor { get; set; }
    
    public int Id_Funcionario { get; set; }
    public int Id_Clientes { get; set; }

    // Dados do INNER JOIN
    public string? NomeFuncionario { get; set; }
    public string? NomeCliente { get; set; }


}




