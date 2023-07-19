using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace RentalCarService.Services
{
    public class ExtraService : IExtraService
    {
        IValidateExtra ValidateExtra;
        private readonly RentalCarsDBContext _dbContext;

        public ExtraService(IValidateExtra validate,RentalCarsDBContext dbContext)
        {
            ValidateExtra = validate;
            _dbContext = dbContext;
        }

        public void InsertNewExtra(Extraa Extra)
        {
            ValidateExtra.Validate(Extra);

            _dbContext.Extras.Add(Extra);
            _dbContext.SaveChanges();
        }

        public List<Extraa> ReadExtrasFromDB()
        {
            var Extras = _dbContext.Extras.ToList();
            return Extras;
        }

        public void DeleteExtraDB(int Id)
        {
            Extraa toRemove = _dbContext.Extras.Find(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdataExtraDB(Extraa Extra)
        {
            ValidateExtra.Validate(Extra);

            Extraa toUpdate = _dbContext.Extras.Find(Extra.Id);
            toUpdate.Service = Extra.Service;
            toUpdate.DayCost = Extra.DayCost;
            toUpdate.FixedCost = Extra.FixedCost;
            _dbContext.SaveChanges();
        }
    }
}
