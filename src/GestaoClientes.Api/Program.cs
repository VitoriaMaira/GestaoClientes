using GestaoClientes.Api.Configurations;
using GestaoClientes.Application;
using GestaoClientes.Infrastructure;
using GestaoClientes.Infrastructure.DataAccess;
using GestaoClientes.Infrastructure.DataAccess.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GestaoClientesDbContext>();
    await DatabaseSeeder.SemearAsync(dbContext);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
