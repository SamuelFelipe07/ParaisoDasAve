using AppExemplo.Components;
using AppExemplo.Configs;
using AppExemplo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<Conexao>();
builder.Services.AddSingleton<ProdutoDAO>();
builder.Services.AddSingleton<AnimalDAO>();
builder.Services.AddSingleton<ClienteDAO>();
builder.Services.AddSingleton<ProdutoDAO>();
builder.Services.AddSingleton<Produto>();
builder.Services.AddSingleton<Funcionario>();
builder.Services.AddSingleton<FuncionarioDAO>();
builder.Services.AddSingleton<Fornecedor>();
builder.Services.AddSingleton<FornecedorDAO>();
builder.Services.AddSingleton<Estoque>();
builder.Services.AddSingleton<EstoqueDAO>();
builder.Services.AddSingleton<Servico>();
builder.Services.AddSingleton<ServicoDAO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
