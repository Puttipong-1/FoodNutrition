using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface IRepositoryBase<T> where T:class, new()
    {
        Task Add(T entity);
        Task AddRange(List<T> entities);
        IQueryable<T> GetAll();
    }
}
