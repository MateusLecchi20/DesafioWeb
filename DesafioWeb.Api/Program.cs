using DesafioWeb.Domain.Clients.Repositories;
using DesafioWeb.Domain.Clients.Services.Interfaces;
using DesafioWeb.Domain.Clients.Services;
using DesafioWeb.Infraestructure.Clients.Repositories;
using DesafioWeb.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly("DesafioWeb.Api")));

builder.Services.AddScoped<IClienteServico, ClienteServico>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();

builder.Services.AddHttpClient<IViaCepServico, ViaCepServico>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/ws/");
});

builder.Services.AddHttpClient<IReceitaWsServico, ReceitaWsServico>(client => 
{
    client.BaseAddress = new Uri("https://receitaws.com.br/v1/cnpj/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
