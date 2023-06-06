using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IBranchsService
    {
        public void InsertNewBranch(Branchs Branch);
        public List<Branchs> ReadBranchsFromDB();
    }
}
