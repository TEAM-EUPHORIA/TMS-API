using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TMS.API.Services;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;

namespace TMS.TEST.Services
{
    public class DepartmentServiceTest
    {

        private readonly Mock<UnitOfWork> _unitOfWork = new Mock<UnitOfWork>();

        private readonly IDepartmentService _departmentService;
        public DepartmentServiceTest()
        {
            _departmentService = new DepartmentService(_unitOfWork.Object);

            _unitOfWork.Setup(obj => obj.Departments.GetDepartments()).Returns(DeparmentMock.GetDepartments());
        }

        [Fact]
        public void GetDepartments()
        {
            var expected = DeparmentMock.GetDepartments();
       
            // Act
            var actual = _departmentService.GetDepartments();
            // Assert
            Assert.Equal(expected,actual);
        }
    }
}