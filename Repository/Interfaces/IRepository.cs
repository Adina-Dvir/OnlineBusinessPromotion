using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    //מכיל את הפונקציות של שליפה מחיקה הוספה ועדכון
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        T AddItem(T item);
        void UpdateItem(int id,T item);
        void DeleteItem(int id);
    }
}
