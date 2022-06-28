using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;

namespace TMS.TEST.Controller
{
    public class RoleControllerTest
    {
        private readonly Mock<ILogger<RoleController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
        private readonly RoleController _roleController;
        private readonly Dictionary<string,string> result = new();
        readonly List<Role> Roles = RoleMock.GetRoles();
        readonly Role Role = RoleMock.GetRole();

       
        public RoleControllerTest()
        {
            _roleController = new RoleController(_unitOfService.Object, _Logger.Object);

            // Arrange
           
            _unitOfService.Setup(obj => obj.RoleService.GetRoles()).Returns(Roles);
            
        }

        [Fact]

        public void GetRoles()
        {
            // Act
            var Result = _roleController.GetRoles() as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
      
        

        public override bool Equals(object? obj)
        {
            return obj is RoleControllerTest test &&
                   EqualityComparer<Role>.Default.Equals(Role, test.Role);
        }
    }
}