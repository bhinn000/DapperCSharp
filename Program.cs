using Dapper;
using DapperC_.Models;
using Microsoft.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.MapControllers();

app.MapGet("customerRoute" , async (IConfiguration configuration) =>
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    using var connection = new SqlConnection(connectionString);

    const string query = "select ID, FirstName, LastName, DateofBirth from Customer";

    var customers = await connection.QueryAsync<Customer>(query);

    return Results.Ok(customers);
});

app.Run();
