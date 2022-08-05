using System.Collections.Generic;
using TMS.API.Services;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
using Microsoft.Extensions.Logging;

namespace TMS.TEST.Services
{
    public class RoleServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly IRoleService _roleService;
        private readonly List<Role> _roles;
        private readonly Role _role;

        public RoleServiceTest(ILogger<RoleService> logger)
        {
            _roleService = new RoleService(_unitOfWork.Object, logger);
            _roles = RoleMock.GetRoles();
            _role = RoleMock.GetRole();
        }
        [Fact]
        public void GetRoles()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Roles.GetRoles()).Returns(_roles);
            // Act
            var result = _roleService.GetRoles();
            // Assert
            Assert.Equal(_roles, result);
        }

    }
}