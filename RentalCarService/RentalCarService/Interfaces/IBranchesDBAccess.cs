using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IBranchesDBAccess
    {
        public Branchs GetBranchById(int id);
    }
}
