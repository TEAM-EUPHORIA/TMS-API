using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using System;

namespace TMS.TEST.Controller
{
    public class UserControllerTest
    {
        private readonly Mock<ILogger<UserController>>_Logger =new ();
        private readonly Mock<IUnitOfService> _unitOfService =new();
        private readonly UserController _userController;
        private readonly Dictionary<string,string> result =new();
        readonly List<User> Users = UserMock.GetAllUserByRole();
        readonly User User = UserMock.GetUserById();
        int role=1;
        int dept=1;
        int id=1;


        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
        public UserControllerTest()
        {
            _userController = new UserController(_unitOfService.Object, _Logger.Object);
            // Arrange
            _unitOfService.Setup(obj => obj.Validation.UserExists(User.Id)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.RoleExists(User.Id)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.DepartmentExists(User.Id)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateUser(User)).Returns(result);

            _unitOfService.Setup(obj => obj.UserService.GetUsersByRole(role)).Returns(Users);
            _unitOfService.Setup(obj => obj.UserService.GetUsersByDepartment(dept)).Returns(Users);
            _unitOfService.Setup(obj => obj.UserService.GetUsersByDeptandrole(dept,role)).Returns(Users);
            _unitOfService.Setup(obj => obj.UserService.GetUserById(id)).Returns(User);
            _unitOfService.Setup(obj => obj.UserService.CreateUser(User)).Returns(result);
            _unitOfService.Setup(obj => obj.UserService.UpdateUser(User)).Returns(result);
            _unitOfService.Setup(obj => obj.UserService.DisableUser(User.Id, 1)).Returns(true);

        }

        [Fact] 
        public void GetUsersRole_Return200Status()
        {
            // Act
            var Result = _userController.GetAllUserByRole(role) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }


        [Fact]

        public void GetUsersRole_Return500Status()
        {
            _unitOfService.Setup(obj => obj.UserService.GetUsersByRole(role)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.GetAllUserByRole(role) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact] 
        public void GetUsersRole_Return404Status()
        {
            _unitOfService.Setup(obj => obj.Validation.RoleExists(User.Id)).Returns(false);
            // Act
            var Result = _userController.GetAllUserByRole(role) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact] 
        public void GetAllUserByDepartment_Return200Status()
        {
            // Act
            var Result = _userController.GetAllUserByDepartment(dept) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }


        [Fact]
        public void GetAllUserByDepartment_Return404Status()
        {

            _unitOfService.Setup(obj => obj.Validation.UserExists(User.Id)).Returns(false);
            // Act
            var Result = _userController.UpdateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetAllUserByDepartment_Return500Status()
        {
            AddIsValid();
            _unitOfService.Setup(obj => obj.UserService.GetUsersByDepartment(dept)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.GetAllUserByDepartment(dept) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact] 
        public void GetUsersByDeptandrole_Return200Status()
        {
            // Act
            var Result = _userController.GetUsersByDeptandrole(dept,role) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetUsersByDeptandrole_Return404Status()
        {

            _unitOfService.Setup(obj => obj.Validation.RoleExists(User.Id)).Returns(false);
            // Act
            var Result = _userController.GetUsersByDeptandrole(dept,role) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetUsersByDeptandrole_Return500Status()
        {
            AddIsValid();
            _unitOfService.Setup(obj => obj.UserService.GetUsersByDeptandrole(dept,role)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.GetUsersByDeptandrole(dept,role) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }


        [Fact]
        public void CreateUsers_Return200Status()
        {
            AddIsValid();
            
            // Act
            var Result = _userController.CreateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        

        [Fact]
        public void CreateUsers_Return400Status_Exist()
        {
            AddExists();
            _unitOfService.Setup(obj => obj.UserService.CreateUser(User)).Returns(result);
            // Act
            var Result = _userController.CreateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        
        [Fact]
        public void CreateUsers_Return400Status()
        {
            
            _unitOfService.Setup(obj => obj.UserService.CreateUser(User)).Returns(result);
            // Act
            var Result = _userController.CreateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }



        [Fact]
        public void CreateUsers_Return500Status()
        {
            AddIsValid();
            _unitOfService.Setup(obj => obj.UserService.CreateUser(User)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.CreateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }


         [Fact]
        public void UpdateUser_Return200Status()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _userController.UpdateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

                 [Fact]
        public void UpdateUser_Return400Status()
        {
            _unitOfService.Setup(obj => obj.UserService.UpdateUser(User)).Returns(result);
            // Act
            var Result = _userController.UpdateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

         [Fact]
        public void UpdateUser_Return500Status()
        {
            AddIsValid();
            AddExists();
            _unitOfService.Setup(obj => obj.UserService.UpdateUser(User)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.UpdateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void UpdateUser_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitOfService.Setup(obj => obj.Validation.UserExists(User.Id)).Returns(false);
            // Act
            var Result = _userController.UpdateUser(User) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }


        [Fact]
        public void DisableUsers_Return200Status()
        {
            // Act  
            var Result = _userController.DisableUser(User.Id) as ObjectResult;
            // Assert
           
         Assert.Equal(200, Result?.StatusCode);
        }

        
        [Fact]
        public void DisableUsers_Return500Status()
        {
             _unitOfService.Setup(obj => obj.UserService.DisableUser(User.Id, 1)).Throws(new InvalidOperationException());
            // Act
            var Result = _userController.DisableUser(User.Id) as ObjectResult;
            // Assert
           
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void DisableUsers_Return404Status()
        {
            _unitOfService.Setup(obj => obj.Validation.UserExists(User.Id)).Returns(false); 
            // Act
            var Result = _userController.DisableUser(User.Id) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        

    }

}