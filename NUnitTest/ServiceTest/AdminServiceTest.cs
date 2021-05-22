using FoodNutrition.Data.Model;
using FoodNutrition.Data.Repository;
using FoodNutrition.Helper;
using FoodNutrition.Service;
using FoodNutrition.Service.Impl;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.ServiceTest
{
    [TestFixture]
    class AdminServiceTest
    {
        private IAdminService adminService;
        Mock<IAdminRepository> mockAdminRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            JwtSetting setting = new JwtSetting { SecretKey="Test-Key" };
            Mock<IOptions<JwtSetting>> mockJwt = new Mock<IOptions<JwtSetting>>(MockBehavior.Strict);
            mockAdminRepository = new Mock<IAdminRepository>(MockBehavior.Strict);
            adminService = new AdminService(mockAdminRepository.Object,mockJwt.Object);
        }
        [Test]
        public async Task AddAdminSuccessful()
        {
            Admin admin = new Admin { };
            Admin mockAdmin = new Admin { };
            mockAdminRepository.Setup(x => x.Add(admin))
                .Returns(Task.CompletedTask);
            mockAdminRepository.Setup(x => x.GetByEmail(admin.Email))
                .ReturnsAsync(mockAdmin);


        }

    }
}
