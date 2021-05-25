using FoodNutrition.Controllers;
using FoodNutrition.Data.DTO.Request;
using FoodNutrition.Data.DTO.Response;
using FoodNutrition.Data.Model;
using FoodNutrition.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Helper;
using System.Linq;

namespace NUnitTest.ControllerTest
{
    [TestFixture]
    class AdminControllerTest
    {
        private AdminController adminController;
        private Mock<IAdminService> mockAdminService;
        private readonly string baseUrl= "https://localhost:44370/api/admin/";
        [OneTimeSetUp]
        public void Setup()
        {
            mockAdminService = new Mock<IAdminService>(MockBehavior.Strict);
            adminController = new AdminController(mockAdminService.Object);
        }
        //Add admin success
        [Test]
        public async Task AddAdminSuccess()
        {
            Admin admin = GetFakeAdmin();
            mockAdminService.Setup(x => x.AddAmin(It.IsAny<Admin>()))
                .ReturnsAsync(true);

            var actionResult = await adminController.Add(admin);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            string message = (string)result.Value;
            Assert.AreEqual("Add successful", message);
        }
        //Add admin fail(email already use)
        [Test]
        public async Task AddAdminFail()
        {
            Admin admin = GetFakeAdmin();
            mockAdminService.Setup(x => x.AddAmin(It.IsAny<Admin>()))
                .ReturnsAsync(false);

            var actionResult = await adminController.Add(admin);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            string message = (string)result.Value;
            Assert.AreEqual("Email already use", message);
        }
        [Test]
        public async Task AdminAuthSuccess()
        {
            AuthenticateRequest request = GetFakeRequest();
            mockAdminService.Setup(x => x.Authenticate(It.IsAny<AuthenticateRequest>()))
                .ReturnsAsync(GetFakeResponse());

            var actionResult = await adminController.Authenticate(request);


            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            AuthenticateResponse response = (AuthenticateResponse)result.Value;
            JwtSecurityToken token = JwtHelper.ValidateJwtToken(response.Token);
            int id = int.Parse(token.Claims.First(x => x.Type == "id").Value);
            Assert.AreEqual(1, id);
            Assert.AreEqual("test@gmail.com", response.Email);
            Assert.AreEqual("AA", response.Name);
        }
        //Admin auth fail(email or password incorrect)
        [Test]
        public async Task AdminAuthFail()
        {
            AuthenticateRequest request = GetFakeRequest();
            mockAdminService.Setup(x => x.Authenticate(It.IsAny<AuthenticateRequest>()))
                .ReturnsAsync((AuthenticateResponse)null);

            var actionResult = await adminController.Authenticate(request);

            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var result = (OkObjectResult)actionResult;
            string msg = (string)result.Value;
            Assert.AreEqual("Email or Password incorrect", msg);
            Assert.AreEqual(200, result.StatusCode);
        }
        private Admin GetFakeAdmin()
        {
            return new Admin
            {
                Name = "AA",
                Email = "test@gmail.com",
                Password = "12345678"
            };
        }
        private AuthenticateResponse GetFakeResponse()
        {
            return new AuthenticateResponse
            {
                Name="AA",
                Email="test@gmail.com",
                Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjE4NTkwNzcsImV4cCI6MTYyMjQ2Mzg3NywiaWF0IjoxNjIxODU5MDc3fQ.31WVz83XHeL8G4nCUIRz9rGMwK7YiDljJhlYSLuptHs"
            };
        }
        private AuthenticateRequest GetFakeRequest()
        {
            return new AuthenticateRequest
            {
                Email = "test@gmail.com",
                Password = "12345678"
            };
        }
    }
}
