using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using PosTech.CadPac.Api.Extensions;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

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
        Title = "PosTech.CadastroPaciente.Api",
        Version = "v1",
        Description = @"Esta api permite listar, criar e atualizar pacientes com seu respectivo histórico médico. </br></br>
                        O projeto foi desenvolvido como parte do curso PÓS TECH ARQUITETURA DE SISTEMAS .NET COM AZURE. </br></br>
                        Mais informações sobre o projeto e como executá-lo podem ser encontradas no repositório do <a target=""_blank"" href=""https://github.com/cyzop/PosTech.CadPac.Api"" rel=""noopener noreferrer"" class=""link"">GitHub</a> </br></br>",

        License = new OpenApiLicense() { 
            Name = "MIT License", 
            Url= new Uri("https://github.com/cyzop/PosTech.CadPac.Api/blob/master/LICENSE") },
       

    });;

    var xmlSummary = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlSummary);

    options.IncludeXmlComments(xmlPath);

    //complementar com Redoc
});

var app = builder.Build();

//app.UseReDoc(c =>
//{
//    c.DocumentTitle = "";
//    c.RoutePrefix = "";
//});

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
