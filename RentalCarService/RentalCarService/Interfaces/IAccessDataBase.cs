using System.Data;

namespace RentalCarService.Interfaces
{
    public interface IAccessDataBase
    {
        public void AccessNonQuery(string Action);
        public IDataReader AccessReader(string Action);
    }
}
