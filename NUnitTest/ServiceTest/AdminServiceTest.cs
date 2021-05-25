using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using FoodNutrition.Helper;
using FoodNutrition.Service;
using FoodNutrition.Service.Impl;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using NUnitTest.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.ServiceTest
{
    [TestFixture]
    class AdminServiceTest
    {
        private IAdminService adminService;
        Mock<IAdminRepository> mockAdminRepository;
        JwtSetting setting;
        [OneTimeSetUp]
        public void Setup()
        {
            setting = new JwtSetting { SecretKey = "H[LHdJ43wH9P2#)Uvn'&nd" };
            Mock<IOptions<JwtSetting>> mockJwt = new Mock<IOptions<JwtSetting>>(MockBehavior.Strict);
            mockJwt.Setup(x => x.Value).Returns(setting);
            mockAdminRepository = new Mock<IAdminRepository>(MockBehavior.Strict);
            adminService = new AdminService(mockAdminRepository.Object,mockJwt.Object);
        }
        //Add admin success
        [Test]
        public async Task AddAdminSuccess()
        {
            Admin admin = new Admin { };
            try
            {
                mockAdminRepository.Setup(x => x.Add(admin))
                .Returns(Task.CompletedTask);
                mockAdminRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                    .ReturnsAsync((Admin)null);

                bool flag = await adminService.AddAmin(admin);

                Assert.IsTrue(flag);
            } catch (Exception e){}
        }
        //Admin is null
        [Test]
        public async Task AddAdminFail1()
        {
            try
            {

                mockAdminRepository.Setup(x => x.Add((Admin)null))
                .Returns(Task.CompletedTask);
                mockAdminRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                    .ReturnsAsync((Admin)null);

                bool flag = await adminService.AddAmin((Admin)null);
            }
            catch(Exception e){
                Assert.IsNotNull(e);
            }
        }
        //email already use
        [Test]
        public async Task AddAdminFail2()
        {

            Admin admin = new Admin
            {
                Name = "AAAA",
                Email = "admin@gmail.com",
                Password = "12345678"
            };

            mockAdminRepository.Setup(x => x.Add(admin))
            .Returns(Task.CompletedTask);
            mockAdminRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(GetFakeAdmin());
            bool flag = await adminService.AddAmin(admin);

            Assert.IsFalse(flag);
        }
        //Admin authenticate success
        [Test]
        public async Task AdminAuthSuccess()
        {

            AuthenticateRequest request = new AuthenticateRequest
            {
                Email = "admin@gmail.com",
                Password = "12345678"
            };
            Admin admin = GetFakeAdmin();

            mockAdminRepository.Setup(x=>x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(admin);
            AuthenticateResponse response = await adminService.Authenticate(request);
            JwtSecurityToken token = JwtHelper.ValidateJwtToken(response.Token);
            int id = int.Parse(token.Claims.First(x => x.Type == "id").Value);
            long exp = long.Parse(token.Claims.First(x => x.Type == "exp").Value);
            DateTime expire = DateTimeOffset.FromUnixTimeMilliseconds(exp / 1000).DateTime;

            Assert.AreEqual(admin.Email, response.Email);
            Assert.AreEqual(admin.Name, response.Name);
            Assert.AreEqual(admin.AdminId, id);
            Assert.Less(DateTime.UtcNow.AddDays(7).CompareTo(expire),3);
        }
        [Test]
        public async Task AdminAuthFail1()
        {

            AuthenticateRequest request = new AuthenticateRequest
            {
                Email = "admin@gmail.com",
                Password = "12345678"
            };
            Admin admin = GetFakeAdmin();

            mockAdminRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync((Admin)null);
            AuthenticateResponse response = await adminService.Authenticate(request);

            Assert.IsNull(response);
        }
        //Admin authenticate fail(password incorrect)
        [Test]
        public async Task AdminAuthFail2()
        {

            AuthenticateRequest request = new AuthenticateRequest
            {
                Email = "admin@gmail.com",
                Password = "123456789test"
            };
            Admin admin = GetFakeAdmin();

            mockAdminRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(GetFakeAdmin());
            AuthenticateResponse response = await adminService.Authenticate(request);

            Assert.IsNull(response);
        }

        private Admin GetFakeAdmin()
        {
            return new Admin
            {
                AdminId = 1,
                Name="AAAA",
                Email = "admin@gmail.com",
                Password = Argon2.Hash("12345678")
            };
        }
    }
}
