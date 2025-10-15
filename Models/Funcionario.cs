namespace AppExemplo.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

public class Funcionario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }

    public string? Rg { get; set; }
    public string? DataNascimento { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }

    public string? Bairro { get; set; }
    public string? Rua { get; set; }
    public int? Numero { get; set; }
    public string? Sexo { get; set; }
    public string? ContaBancaria { get; set; }
}

