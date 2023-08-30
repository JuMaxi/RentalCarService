using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class BranchesDBAccess : IBranchesDBAccess
    {
        readonly RentalCarsDBContext _dbcontext;
        public BranchesDBAccess(RentalCarsDBContext dBcontext)
        {
            _dbcontext = dBcontext;
        }

        public Branchs GetBranchById(int id)
        {
            var branch = _dbcontext.Branches.Include(O => O.OpeningHours).Where(B => B.Id == id).FirstOrDefault();
            return branch;
        }
    }
}
