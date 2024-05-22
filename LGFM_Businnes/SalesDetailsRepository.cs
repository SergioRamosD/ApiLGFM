using System.Data.SqlClient;
using LGFM_Abstraction;
using LGFM_Entities;

namespace LGFM_Businnes;

public class SalesDetailsRepsitory : ISalesDetails
{
    private readonly string _connectionString;
    public SalesDetailsRepsitory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<ICollection<SalesDetails>> GetAll()
    {
        var SalesDetails = new List<SalesDetails>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = "SELECT * FROM SalesDetails";
            using (var command = new SqlCommand(query,connection))
            { 
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var salesDetails = new SalesDetails();
                    salesDetails.DetalleVentaID = reader.GetInt32(0);
                    salesDetails.VentaID = reader.GetInt32(1);
                    salesDetails.ProductoID = reader.GetInt32(2);
                    salesDetails.Cantidad = reader.GetInt32(3);
                    salesDetails.PrecioUnitario = reader.GetDecimal(4);
                    salesDetails.Total = reader.GetDecimal(5);

                    SalesDetails.Add(salesDetails);
                }
            }
        }
        return SalesDetails;
    }
}