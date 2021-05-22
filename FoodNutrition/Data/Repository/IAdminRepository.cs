using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository
{
    public interface IAdminRepository:IRepositoryBase<Admin>
    {
        Task<Admin> GetByEmail(string email);
        Admin GetById(int id);
    }
}
