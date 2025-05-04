using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    //הלוגיקה העיסקית כאן
    public interface IService<T>
    {
        T GetById(int id);
        List<T> GetAll();
        T AddItem(T item);
        void UpdateItem(int id, T item);
        void DeleteItem(int id);
    }
}
