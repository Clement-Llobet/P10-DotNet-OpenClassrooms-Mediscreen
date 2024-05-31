using Mediscreen.API.Endpoints;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Infrastructure.Config;
using Mediscreen.Infrastructure.SqlServerDatabase;
using Mediscreen.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var sqlServerConnectionString = builder.Configuration.GetConnectionString("SqlServerContext");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSqlServerDatabase(sqlServerConnectionString!);
builder.Services.AddScoped<IPatientRepository>(provider => provider.GetRequiredService<MediscreenSqlServerContext>().PatientRepository);

builder.Services.AddTransient<BogusDatasGenerator>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mediscreen", Version = "v1" });
});


var app = builder.Build();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var MediscreenSqlServerContext = serviceScope.ServiceProvider.GetRequiredService<MediscreenSqlServerContext>();
    MediscreenSqlServerContext.Database.Migrate();
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
