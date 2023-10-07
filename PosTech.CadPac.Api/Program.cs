using Microsoft.OpenApi.Models;
using PosTech.CadPac.Api.Extensions;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//List<PacienteDto> pacientes = new List<PacienteDto>();

////1 - pacientes
//pacientes.Add(new PacienteDto() { Id = "9d311731-8837-4dfb-ab5f-940304e3933a", Nome = "José da silva", DataNascimento = new DateTime(1999,10,12), Email = "jose.silva@seuemail.com", Responsavel = "Maria José da Silva", Historico = new List<LancamentoMedicoDto>() });
//pacientes.Add(new PacienteDto() { Id = "b3e17a87-68e8-45b8-8b91-1e7dc8da5843", Nome = "Maria Aparecida", DataNascimento = new DateTime(2000,10,02), Email = "maria.aparecida@seuemail.com", Responsavel = "", Historico = new List<LancamentoMedicoDto>() });
//pacientes.Add(new PacienteDto() { Id = "facd9c1c-c0a8-4d39-8ad7-2e628c6b7572", Nome = "Fernando Henrique", DataNascimento = new DateTime(2010,10,02), Email = "fernandohenrique@seuemail.com", Responsavel = "", Historico = new List<LancamentoMedicoDto>() });
//pacientes.Add(new PacienteDto() { Id = "21184bf5-0ce4-4528-851a-ebd76928f8cb", Nome = "Barbara Heleonora", DataNascimento = new DateTime(2004,10,02), Email = "babiheleonora@seuemail.com", Responsavel = "", Historico = new List<LancamentoMedicoDto>() });

////2 - hitorico
//pacientes[0].Historico.Add(new LancamentoMedicoDto() { Data = DateTime.Now, Id = Guid.NewGuid().ToString(), Tipo = TipoLancamentoMedicoDto.Sintoma, Texto = "Dor nos olhos" });
//pacientes[0].Historico.Add(new LancamentoMedicoDto() { Data = DateTime.Now, Id = Guid.NewGuid().ToString(), Tipo = TipoLancamentoMedicoDto.Diagnostico, Texto = "Conjuntivite" });
//pacientes[0].Historico.Add(new LancamentoMedicoDto() { Data = DateTime.Now, Id = Guid.NewGuid().ToString(), Tipo = TipoLancamentoMedicoDto.Sintoma, Texto = "Colírio de 4 em 4 horas" });

builder.Host.ConfigureServices(services =>
{
    services.AddDependencyInjection(configuration);
});


builder.Services.AddControllers()
         .ConfigureApiBehaviorOptions(options =>
         {
             options.SuppressModelStateInvalidFilter = true;
         });

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PosTech.CadPac.Api",
        Version = "v1"
    });
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
