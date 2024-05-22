using System.Data.SqlClient;
using LGFM_Abstraction;
using LGFM_Entities;

namespace LGFM_Businnes
{
    public class SalesRepository : ISales
    {
        private readonly string _connectionString;
        public SalesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<ICollection<Sales>> GetAll()
        {
            var Sales = new List<Sales>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Sales";

                using (var command = new SqlCommand(query, connection))
                {
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var sales = new Sales();
                        sales.VentaID = reader.GetInt32(0);
                        sales.FechaVenta = reader.GetDateTime(1);
                        sales.TotalVenta = reader.GetDecimal(2);
                        
                        Sales.Add(sales);
                    }
                }
            }

            return Sales;
        }
    }
}
