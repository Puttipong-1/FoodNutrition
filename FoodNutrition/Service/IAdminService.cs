using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Service
{
    public interface IAdminService
    {
        Task AddAmin(Admin admin);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Admin GetById(int id);
    }
}
