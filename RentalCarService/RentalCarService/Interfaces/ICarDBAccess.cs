using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface ICarDBAccess
    {
        public Car GetCarByCategoryId(int id);
        public int GetCountFleetByCategoryId(int id);
    }
}
