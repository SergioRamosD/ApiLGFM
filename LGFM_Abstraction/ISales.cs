using LGFM_Entities;

namespace LGFM_Abstraction
{
    public interface ISales
    {
        Task<ICollection<Sales>> GetAll();
    }
}
