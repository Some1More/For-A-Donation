using For_A_Donation.Models.DataBase;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IFamilyService
{
    Family GetById(int id);

    Task<Family> Create(Family family);

    Task Delete(int id);
}
