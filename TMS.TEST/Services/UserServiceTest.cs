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


namespace TMS.TEST.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<Statistics> _statistics = new();
        private readonly IUserService _userService;
        private readonly List<User> _users;
        private readonly User _user ;
        private readonly Dictionary<string,string> result = new();
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
        public UserServiceTest()
       {
        _userService = new UserService(_unitOfWork.Object);
            _users= UserMock.GetAllUserByRole();
            _user= UserMock.GetUserById();

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
            Assert.Equal(_users,result);
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
            Assert.Equal(_users,result);
        }

        [Fact]
        public void GetUsersByDeptandrole()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.RoleExists(role)).Returns(true);
            _unitOfWork.Setup(obj => obj.Validation.DepartmentExists(dept)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUsersByDeptandrole(role,dept)).Returns(_users);
            // Act
            var result = _userService.GetUsersByDeptandrole(role,dept);
            // Assert
            Assert.Equal(_users,result);
        }

        [Fact]
        public void GetUserById()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.UserExists(id)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUserById(id)).Returns(_user);
            // Act
            var result = _userService.GetUserById(id);
            // Assert
            Assert.Equal(_user,result);
        }
    [Fact]
    public void CreateUser()

    {

      AddIsValid();

     _unitOfWork.Setup(obj => obj.Validation.ValidateUser(_user)).Returns(result);
     _unitOfWork.Setup(obj => obj.Stats.GetUserCount()).Returns(0);

     _unitOfWork.Setup(obj => obj.Users.CreateUser(_user));

     _unitOfWork.Setup(obj => obj.Complete());

     var Result = _userService.CreateUser(_user);

     Assert.Equal(result,Result);

    }

    [Fact]
    public void UpdateUser()

    {

      AddIsValid();

     _unitOfWork.Setup(obj => obj.Validation.ValidateUser(_user)).Returns(result);
     _unitOfWork.Setup(obj => obj.Stats.GetUserCount()).Returns(0);

     _unitOfWork.Setup(obj => obj.Users.UpdateUser(_user));

     _unitOfWork.Setup(obj => obj.Complete());

     var Result = _userService.UpdateUser(_user);

     Assert.Equal(result,Result);

    }

    [Fact]
    public void DisableUser()

    {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.UserExists(id)).Returns(true);
            _unitOfWork.Setup(obj => obj.Users.GetUserById(id)).Returns(_user);
            // Act
            var result = _userService.GetUserById(cid);
            // Assert
            Assert.Equal(_user,result);
      

    }
    
    



    }
}