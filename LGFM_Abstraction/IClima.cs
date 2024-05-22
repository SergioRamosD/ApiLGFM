using LGFM_Entities;

namespace LGFM_Abstraction
{
    public interface IClima
    {
        Task<ICollection<Clima>> GetAll();
    }
}
