// See https://aka.ms/new-console-template for more information
// origem dos endpoints: https://deividfortuna.github.io/fipe/

using ImportaDados.IntegrationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcMarcaModelo.Data;
using MvcMarcaModelo.Models;

var svc = new MarcasService();

// get config
var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = configurationBuilder.Build();
string connectionString = configuration.GetConnectionString("VehicleDataBase");

// prepare EfCore to run
var services = new ServiceCollection();


services.AddDbContext<MvcMarcaModeloContext>(options =>
    options.UseSqlServer(connectionString));

var serviceProvider = services.BuildServiceProvider();
var _dbContext = serviceProvider.GetService<MvcMarcaModeloContext>();

// incluir dados

var listaMarcas = await svc.GetMarcas();

foreach (var item in listaMarcas)
{
    var marcaModel = new MarcaModel() { Nome = item.Nome };
    await _dbContext.Marcas.AddAsync(marcaModel);
    _dbContext.SaveChanges();

    var listaModelos = await svc.GetModelos(item.Codigo.ToString(), marcaModel.Id);
    await _dbContext.Modelos.AddRangeAsync(listaModelos);
    _dbContext.SaveChanges();
}

/*
var cadastro1 = new MarcaModel { Nome = "HONDA" };

_dbContext.Marcas.Add(cadastro1);
await _dbContext.SaveChangesAsync();
*/