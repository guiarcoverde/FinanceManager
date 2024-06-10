using FinanceManager.API.Filters;
using FinanceManager.API.Middleware;
using FinanceManager.Application;
using FinanceManager.Infrastructure;
using FinanceManager.Infrastructure.DataAccess;
using FinanceManager.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigrateDatabse();

app.Run();

async Task MigrateDatabse()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigrations.MigrateDatabase(scope.ServiceProvider);
    
}