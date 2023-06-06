using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BranchsController : ControllerBase
    {
        IBranchsService BranchsService;
        public BranchsController(IBranchsService Branchs) 
        {
            BranchsService = Branchs;
        }

        [HttpPost]
        public void InsertNewBranch(Branchs Branch)
        {
            BranchsService.InsertNewBranch(Branch);
        }

        [HttpGet]
        public List<Branchs> ReadBranchsFromDB()
        {
            List<Branchs> ListBranchs = BranchsService.ReadBranchsFromDB();
            return ListBranchs;
        }
    }
}
