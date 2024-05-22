using LGFM_Entities;

namespace LGFM_Abstraction
{
    public interface IProduct
    {
        Task<ICollection<Product>> GetAll();
        Task<Product> Add(Product product);
    }
}
