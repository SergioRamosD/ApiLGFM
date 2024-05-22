using LGFM_Entities;

namespace LGFM_Abstraction;

public interface ISalesDetails
{
    Task<ICollection<SalesDetails>> GetAll();
}