using FoodNutrition.Data;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.Model;
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
        private readonly ApplicationDbContext dbContext;
        private readonly JwtSetting jwtSetting;
        public AdminService(ApplicationDbContext _dbContext,IOptions<JwtSetting> _jwtSetting)
        {
            dbContext = _dbContext;
            jwtSetting = _jwtSetting.Value;
        }

        public async Task AddAmin(Admin admin)
        {
            try
            {
                Admin a = await dbContext.Admins.SingleOrDefaultAsync(a => a.Email.Equals(admin.Email));
                if (a is null)
                {
                    admin.Password = Argon2.Hash(admin.Password);
                    await dbContext.AddAsync(admin);
                    await dbContext.SaveChangesAsync();
                }
                else throw new Exception("Email already use");

            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            try
            {
                Admin admin = await dbContext.Admins.FirstAsync(a => a.Email.Equals(request.Email));
                if (!Argon2.Verify(admin.Password, request.Password)) throw new Exception("Password in correct");
                string token = GenerateJwtToken(admin);
                return new AuthenticateResponse(admin, token);
            }
            catch (Exception e)
            {
                throw;
            }
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
            return dbContext.Admins.FirstOrDefault(a => a.AdminId==id);
        }
    }
}
