using FoodNutrition.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodNutrition.Helper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly JwtSetting jwtSetting;
        public JwtMiddleware(RequestDelegate _next,IOptions<JwtSetting> _jwtSetting)
        {
            next = _next;
            jwtSetting = _jwtSetting.Value;
        }
        public async Task Invoke(HttpContext context,IAdminService adminService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token != null)
                AttachUserToContext(context, adminService, token);
            await next(context);
        }
        private void AttachUserToContext(HttpContext context,IAdminService adminService,string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(jwtSetting.SecretKey);
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validateToken) ;
                JwtSecurityToken jwtToken = (JwtSecurityToken)validateToken;
                int adminId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                context.Items["Admin"] = adminService.GetById(adminId);
            }
            catch(Exception e)
            {
            }
        }
    }
}
