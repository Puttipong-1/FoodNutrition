using FoodNutrition.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.Repository.Impl
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Admin> GetByEmail(string email)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Email == email);
        }
        public Admin GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.AdminId == id);
        }
    }
}
