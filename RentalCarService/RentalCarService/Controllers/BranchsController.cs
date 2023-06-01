using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;

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
    }
}
