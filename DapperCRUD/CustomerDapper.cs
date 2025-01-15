using Microsoft.Data.SqlClient;
using Dapper;

namespace DapperCRUD
{
    public class CustomerDapper
    {
        public void DapperConcept()
        {
            using(SqlConnection connection=new SqlConnection("DefaultConnection"))
            {
                //retrieving single output from the query
                    var sql1 = "SELECT COUNT(*) FROM Customer";
                    var count = connection.ExecuteScalar(sql1);
                    //var count = connection.ExecuteScalar<int>(sql);
                    //var count = await connection.ExecuteScalarAsync(sql);
                    //var count = await connection.ExecuteScalarAsync<int>(sql);
                    Console.WriteLine($"Total products: {count}");

                //querying single row
                    var sql2 = "SELECT * FROM Customer WHERE Id= @Id";
                    var customer = connection.QuerySingle(sql2, new { Id= 1 });
                    //var customer = connection.QuerySingle<Customer>(sql2, new { Id = 1 });
                    Console.WriteLine($"ProductID: {customer.ID}");

                //retrieving multiple rows
                    var sql = "SELECT * FROM Customer WHERE FirstName = @FirstName";
                    var customers = connection.Query<Customer>(sql, new { FirstName = "Ram" }).ToList();
                    foreach (var c in customers)
                    {
                        Console.WriteLine($" Name: {c.FirstName}");
                    }

                //Querying multiple : Execute multiple queries within a single database command and return a GridReader to map the results to multiple objects
                //you can select multiple rows from different tables
                        string sql3 = @"
                                    SELECT * FROM [Customer] WHERE Id = @Id;
                                    SELECT * FROM [Customer2] WHERE Id = @Id;
                         ";

                        using (var multi = connection.QueryMultiple(sql3, new { ID = 101 }))
                        {
                            var customer1 = multi.ReadFirst<Customer>();
                            var customer2 = multi.Read<Customer2>().ToList();

                            FiddleHelper.WriteTable("Order", new List<Customer>() { customer1 });
                            FiddleHelper.WriteTable("OrderItem", customer2);
                        }



            }

        }
        static void Main()
        {
            CustomerDapper cd = new CustomerDapper();
        }
       
    }
}
