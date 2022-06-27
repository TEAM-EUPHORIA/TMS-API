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
    public class DepartmentControllerTest
    {
        private readonly Mock<ILogger<DepartmentController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
        private readonly DepartmentController _departmentController;
        private readonly Dictionary<string,string> result = new();
        readonly List<Department> Departments = DeparmentMock.GetDepartments();
        readonly Department Department = DeparmentMock.GetDepartment();
        int departmentId = 1;

        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
        public DepartmentControllerTest()
        {
            _departmentController = new DepartmentController(_unitOfService.Object, _Logger.Object);

            // Arrange
            _unitOfService.Setup(obj => obj.Validation.DepartmentExists(Department.Id)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateDepartment(Department)).Returns(result);

            _unitOfService.Setup(obj => obj.DepartmentService.GetDepartments()).Returns(Departments);
            _unitOfService.Setup(obj => obj.DepartmentService.GetDepartmentById(departmentId)).Returns(Department);
            _unitOfService.Setup(obj => obj.DepartmentService.CreateDepartment(Department)).Returns(result);
            _unitOfService.Setup(obj => obj.DepartmentService.UpdateDepartment(Department)).Returns(result);
            _unitOfService.Setup(obj => obj.DepartmentService.DisableDepartment(Department.Id, 1)).Returns(true);

        }

        [Fact]
        public void GetDepartments()
        {
            // Act
            var Result = _departmentController.GetDepartments() as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetDepartmentById()
        {
            // Act
            var Result = _departmentController.GetDepartmentById(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void CreateDepartment()
        {
            AddIsValid();
            // Act
            var Result = _departmentController.CreateDepartment(Department) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void UpdateDepartment()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _departmentController.UpdateDepartment(Department) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void DisableDepartment()
        {
            // Act
            var Result = _departmentController.DisableDepartment(Department.Id) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
    }
}