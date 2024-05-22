using System.Data.SqlClient;
using LGFM_Abstraction;
using LGFM_Entities;

namespace LGFM_Businnes;

public class ProductRepository : IProduct
{
    private readonly string _connectionString;
    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<ICollection<Product>> GetAll()
    {
        var products = new List<Product>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            // Consulta SQL para obtener todos los productos
            var query = "SELECT * FROM Product";

            using (var command = new SqlCommand(query, connection))
            {
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var product = new Product();

                    product.ProductoID = reader.GetInt32(0);
                    product.ProductName = reader.GetString(1);
                    product.ProductNumber = reader.GetString(2);
                    product.Category = reader.GetString(3);
                    product.SalePrice = reader.GetDecimal(4);
                    product.PricePurchase = reader.GetDecimal(5);
                    product.Stock = reader.GetInt32(6);
                    product.UnidadMedida = reader.GetString(7);
                    product.Supplier = reader.GetString(8);
                    product.Estatus = reader.GetBoolean(9);
                    product.CreationDateProduct = reader.GetDateTime(10);

                    // Verificar si DateModifiedProduct es nulo
                    product.DateModifiedProduct = !reader.IsDBNull(11) ? reader.GetDateTime(11) : (DateTime?)null;

                    products.Add(product);
                }
            }
        }

        return products;
    }

    public async Task<Product> Add(Product product)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @"
            INSERT INTO Product (ProductName, ProductNumber, Category, SalePrice, PricePurchase, Stock, UnidadMedida, Supplier, Estatus, CreationDateProduct, DateModifiedProduct)
            OUTPUT INSERTED.ProductoID
            VALUES (@ProductName, @ProductNumber, @Category, @SalePrice, @PricePurchase, @Stock, @UnidadMedida, @Supplier, @Estatus, @CreationDateProduct, @DateModifiedProduct)";

            using (var command = new SqlCommand(query, connection))
            {
                // Añadir parámetros para evitar SQL Injection
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductNumber", product.ProductNumber);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                command.Parameters.AddWithValue("@PricePurchase", product.PricePurchase);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@UnidadMedida", product.UnidadMedida);
                command.Parameters.AddWithValue("@Supplier", product.Supplier);
                command.Parameters.AddWithValue("@Estatus", product.Estatus);
                command.Parameters.AddWithValue("@CreationDateProduct", product.CreationDateProduct);
                command.Parameters.AddWithValue("@DateModifiedProduct", product.DateModifiedProduct.HasValue ? (object)product.DateModifiedProduct.Value : DBNull.Value);

                // Ejecutar la consulta y obtener el ID del producto insertado
                product.ProductoID = (int)await command.ExecuteScalarAsync();
            }
        }

        return product;
    }
}
