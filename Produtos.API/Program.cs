using Infraestrutura.BancoDados;
using Produtos.API.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainDependencies();

builder.Services.AddInfraDependencies(builder.Configuration);

var assemblies = new Assembly[] {
    Assembly.Load("Dominio"),
    Assembly.Load("Infraestrutura"),
    Assembly.Load("Aplicacao")
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseUpdateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
