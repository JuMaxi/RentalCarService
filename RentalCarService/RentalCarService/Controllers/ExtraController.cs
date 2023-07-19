using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ExtraController : ControllerBase
    {
        IExtraService ExtraService;
        public ExtraController(IExtraService extraService)
        { 
            ExtraService= extraService;
        }

        [HttpPost]
        public void InsertNewExtra(Extraa Extra)
        {
            ExtraService.InsertNewExtra(Extra);
        }

        [HttpGet]
        public List<Extraa> ReadExtrasFromDB()
        {
            List<Extraa> Extras = ExtraService.ReadExtrasFromDB();
            return Extras;
        }

        [HttpDelete]
        public void DeleteExtra([FromQuery] int id)
        {
            ExtraService.DeleteExtraDB(id);
        }

        [HttpPut]
        public void UpdateExtraDB(Extraa Extra)
        {
            ExtraService.UpdataExtraDB(Extra);
        }
    }
}
