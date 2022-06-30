using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Services;
using TMS.API.Repositories;
using Moq;
using TMS.API;
using Xunit;
using TMS.BAL;

namespace TMS.TEST.Services
{
    public class DepartmentServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly IDepartmentService _departmentService;
        private readonly List<Department> _departments;
        private readonly Department _department;

        public DepartmentServiceTest()
        {
            _departmentService = new DepartmentService(_unitOfWork.Object);
            _departments = DeparmentMock.GetDepartments();
            _department = DeparmentMock.GetDepartment();
        }
        [Fact]
        public void GetDepartments()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Departments.GetDepartments()).Returns(_departments);
            // Act
            var result = _departmentService.GetDepartments();
            // Assert
            Assert.Equal(_departments,result);
        }
        [Fact]
        public void GetDepartmentById()
        {
            // Arrange
            _unitOfWork.Setup(obj => obj.Validation.DepartmentExists(_department.Id!)).Returns(true);
            _unitOfWork.Setup(obj => obj.Departments.GetDepartmentById(_department.Id)).Returns(_department);
            // Act
            var result = _departmentService.GetDepartmentById(_department.Id);
            // Assert
            Assert.Equal(_department,result);
        }
    }
}