using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using FoodNutrition.Helper;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodNutrition.Service.Impl
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        private readonly JwtSetting jwtSetting;
        public AdminService(IAdminRepository _adminRepository, IOptions<JwtSetting> _jwtSetting)
        {
            adminRepository = _adminRepository;
            jwtSetting = _jwtSetting.Value;
        }

        public async Task<bool> AddAmin(Admin admin)
        {
            Admin ad = await adminRepository.GetByEmail(admin.Email);
            if (ad is null)
            {
                admin.Password = Argon2.Hash(admin.Password);
                await adminRepository.Add(admin);
                return true;
            }
            else return false;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
                Admin admin = await adminRepository.GetByEmail(request.Email);
                if (admin is null|| !Argon2.Verify(admin.Password, request.Password)) return null;
                string token = GenerateJwtToken(admin);
                return new AuthenticateResponse(admin, token);
        }

        private string GenerateJwtToken(Admin admin)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", admin.AdminId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Admin GetById(int id)
        {
            return adminRepository.GetById(id);
        }
    }
}
