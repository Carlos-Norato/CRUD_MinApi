using Dapper;
using MiniApi.Interfaces;
using MiniApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using MiniApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();

var app = builder.Build();



app.MapGet("/fornecedores", ([FromServices]IFornecedorRepository repository) => 
{
    return repository.GetFornecedors();     
});

app.MapGet("/fornecedor", ([FromServices]IFornecedorRepository repository, int id) => 
{
    return repository.GetFornecedor(id);     
});

app.MapPost("/fornecedor",([FromServices]IFornecedorRepository repository, FornecedorModel fornecedor) => 
{
    var result = repository.InsertFornecedor(fornecedor);
    return result ? Results.Created("aaaaaa", fornecedor) : Results.BadRequest();

});

app.MapPut("/fornecedor", ([FromServices]IFornecedorRepository repository, int id, string desc) => 
{
    var result = repository.UpdateFornecedorDesc(desc,id);
    return result ? Results.Ok("Descrição alterada") : Results.BadRequest();
});

app.MapDelete("/fornecedor",([FromServices]IFornecedorRepository repository, int id) => 
{
    var result = repository.Delete(id);
    return result ? Results.Ok("Fornecedor deletado") : Results.BadRequest();
});


app.MapGet("/produtos", ([FromServices]IProdutoRepository repository, int page, int tamanho, string order, string search) => 
{
    return repository.GetProdutos(page, tamanho, order, search);
});

app.MapGet("/produto", ([FromServices]IProdutoRepository repository, int id) => 
{
    return repository.GetProduto(id);
});

app.MapPost("/produto",([FromServices]IProdutoRepository repository, string descricao, DateTime data_fab, DateTime data_val, int id_forn) =>
{
    var result = repository.InsertProduto(descricao, data_fab, data_val, id_forn);
    return result ? Results.Ok() : Results.BadRequest();
}); 

app.MapDelete("/produto", ([FromServices]IProdutoRepository repository, int id) =>
{
    var result = repository.Delete(id);
    return result ? Results.Ok() : Results.BadRequest();
});

app.MapPut("/produto", ([FromServices]IProdutoRepository repository, int id_prod,string descricao, DateTime data_fab, DateTime data_val, int id_forn) => 
{
    var result = repository.Update(id_prod, descricao, data_fab, data_val, id_forn);
    return result ? Results.Ok() : Results.BadRequest();
});

app.Run();
