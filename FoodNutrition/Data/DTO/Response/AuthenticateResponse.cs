using FoodNutrition.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNutrition.Data.DTO.Response
{
    public class AuthenticateResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public AuthenticateResponse() { }
        public AuthenticateResponse(Admin admin,string token) {
            Name = admin.Name;
            Email = admin.Email;
            Token = token;
        }
    }
}
