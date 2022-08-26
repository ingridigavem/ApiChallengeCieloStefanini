using ApiChallenge.Data.Repositories;
using ApiChallenge.Domain.Interfaces.Repositories;
using ApiChallenge.Domain.Interfaces.Services;
using ApiChallenge.Services;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMainRepository, MainRepository>();
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddTransient<IDbConnection>((d) => new SqlConnection(builder.Configuration.GetConnectionString("DBConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Challenge V1");
        c.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
