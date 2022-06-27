using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using TMS.API;
using TMS.API.ViewModels;

namespace TMS.TEST.Controller
{
    public class AuthControllerTest
    {
        private readonly Mock<ILogger<AuthController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
        private readonly AuthController _authController;
        private readonly Dictionary<string,string> result = new();
     
        readonly LoginModel user = AuthMock.Login();

        private void token()
        {
            result.Add("token", "");
        }
        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
       
        public AuthControllerTest()
        {
            _authController = new AuthController(_unitOfService.Object, _Logger.Object);

            // Arrange
        
            _unitOfService.Setup(obj => obj.Validation.ValidateLoginDetails(user)).Returns(result);

            _unitOfService.Setup(obj => obj.AuthService.Login(user)).Returns(result);
           

        }

        [Fact]

        public void Login()
        {
            AddIsValid();
            // Act
            var Result = _authController.Login(user) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
       
        
    }
}