using PosTech.CadPac.Api.Models;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

List<Paciente> pacientes = new List<Paciente>();

pacientes.Add(new Paciente() { Id = "9d311731-8837-4dfb-ab5f-940304e3933a", Nome = "José da silva", DataNascimento = new DateTime(20001002), Email = "jose.silva@seuemail.com", Responsavel = "Maria José da Silva" });
pacientes.Add(new Paciente() { Id = "b3e17a87-68e8-45b8-8b91-1e7dc8da5843", Nome = "Maria Aparecida", DataNascimento = new DateTime(20001002), Email = "maria.aparecida@seuemail.com", Responsavel = "" });
pacientes.Add(new Paciente() { Id = "facd9c1c-c0a8-4d39-8ad7-2e628c6b7572", Nome = "Fernando Henrique", DataNascimento = new DateTime(20001002), Email = "fernandohenrique@seuemail.com", Responsavel = "" });
pacientes.Add(new Paciente() { Id = "21184bf5-0ce4-4528-851a-ebd76928f8cb", Nome = "Barbara Heleonora", DataNascimento = new DateTime(20001002), Email = "babiheleonora@seuemail.com", Responsavel = "" });


builder.Services.AddSingleton<List<Paciente>>(pacientes);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
