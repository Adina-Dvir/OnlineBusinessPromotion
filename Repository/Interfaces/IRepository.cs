﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    //מכיל את הפונקציות של שליפה מחיקה הוספה ועדכון
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task< List<T>> GetAll();
        Task< T> AddItem(T item);
        Task UpdateItem(int id,T item);
        Task DeleteItem(int id);
    }
}
