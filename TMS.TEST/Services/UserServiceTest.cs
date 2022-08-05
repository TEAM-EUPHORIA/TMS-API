using System.Collections.Generic;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
using Microsoft.Extensions.Logging;
using TMS.API.ViewModels;

namespace TMS.TEST.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<Statistics> _statistics = new();
        private readonly IUserService _userService;
        private readonly List<User> _users;
        private readonly User _user;
        private readonly UpdateUserModel _updateUser;
        private readonly Dictionary<string, string> result = new();
        int dept;
        int role;
        int id;
        int cid;
        private void AddIsValid()
        {

            result.Add("IsValid", "true");

        }

        private void AddExists()

        {

            result.Add("Exists", "true");

        }
        public UserServiceTest(ILogger<UserService> logger)
        {
            _userService = new UserService(_unitOfWork.Object, logger);
            _users = UserMock.GetAllUserByRole();
            _user = UserMock.GetUserById();
            _updateUser = UserMock.GetUpdateUser();

        }

        [Fact]
        public void GetUsersByDepartment()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.DepartmentExists(dept)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUsersByDepartment(dept)).Returns(_users);
            // Act
            var result = _userService.GetUsersByDepartment(dept);
            // Assert
            Assert.Equal(_users, result);
        }

        [Fact]
        public void GetUsersByRole()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.RoleExists(role)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUsersByRole(role)).Returns(_users);
            // Act
            var result = _userService.GetUsersByRole(role);
            // Assert
            Assert.Equal(_users, result);
        }

        [Fact]
        public void GetUsersByDeptandrole()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.RoleExists(role)).Returns(true);
            _unitOfWork.Setup(obj => obj.Validation.DepartmentExists(dept)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUsersByDeptandRole(dept, role)).Returns(_users);
            // Act
            var result = _userService.GetUsersByDeptandRole(dept, role);
            // Assert
            Assert.Equal(_users, result);
        }

        [Fact]
        public void GetUserById()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.UserExists(id)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUserById(id)).Returns(_user);
            // Act
            var result = _userService.GetUser(id);
            // Assert
            Assert.Equal(_user, result);
        }
        [Fact]
        public void CreateUser()

        {

            AddIsValid();

            _unitOfWork.Setup(obj => obj.Validation.ValidateUser(_user)).Returns(result);
            _unitOfWork.Setup(obj => obj.Stats.GetUserCount()).Returns(0);

            _unitOfWork.Setup(obj => obj.Users.CreateUser(_user));

            _unitOfWork.Setup(obj => obj.Complete());

            var Result = _userService.CreateUser(_user,1);

            Assert.Equal(result, Result);

        }

        [Fact]
        public void UpdateUser()

        {

            AddIsValid();

            _unitOfWork.Setup(obj => obj.Validation.ValidateUser(_user)).Returns(result);
            _unitOfWork.Setup(obj => obj.Stats.GetUserCount()).Returns(0);

            _unitOfWork.Setup(obj => obj.Users.UpdateUser(_user));

            _unitOfWork.Setup(obj => obj.Complete());

            var Result = _userService.UpdateUser(_updateUser,1);

            Assert.Equal(result, Result);

        }

        [Fact]
        public void DisableUser()

        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.UserExists(id)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUserById(id)).Returns(_user);
            // Act
            var result = _userService.GetUser(cid);
            // Assert
            Assert.Equal(_user, result);


        }





    }
}