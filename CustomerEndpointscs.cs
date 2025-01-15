using Dapper;
using DapperC_.Models;
using Microsoft.Data.SqlClient;

namespace DapperC_
{
    public static class CustomerEndpointscs
    {
        public static void MapCustomerEndPoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("customerRoute", async (IConfiguration configuration) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                using var connection = new SqlConnection(connectionString);

                const string query = "select ID, FirstName, LastName, DateofBirth from Customer";

                var customers = await connection.QueryAsync<Customer>(query);

                return Results.Ok(customers);
            });
        }
    }
}
