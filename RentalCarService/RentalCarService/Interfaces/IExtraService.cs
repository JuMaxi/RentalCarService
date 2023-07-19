using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IExtraService
    {
        public void InsertNewExtra(Extraa Extra);
        public List<Extraa> ReadExtrasFromDB();
        public void DeleteExtraDB(int Id);
        public void UpdataExtraDB(Extraa Extra);
    }
}
