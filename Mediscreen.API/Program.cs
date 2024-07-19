using Mediscreen.API.Endpoints;
using Mediscreen.Infrastructure.Config;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Mediscreen.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerContext");
//var mongoDbConnectionStringFile = builder.Configuration["MONGO_CONNECTION_STRING_FILE"];
//string connectionString = File.ReadAllText(mongoDbConnectionStringFile).Trim();
var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSqlServerDatabase(sqlServerConnectionString!);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Mediscreen";
});

builder.Services.AddTransient<BogusDatasGenerator>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mediscreen", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var MediscreenSqlServerContext = serviceScope.ServiceProvider.GetRequiredService<MediscreenSqlServerContext>();
    MediscreenSqlServerContext.Database.Migrate();

    var identityContext = serviceScope.ServiceProvider.GetRequiredService<IdentityContext>();
    identityContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapPatientsEndpoints();

app.Run();
