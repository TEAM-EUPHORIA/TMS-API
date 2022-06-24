using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using TMS.API;

namespace TMS.TEST.Controller
{
    public class Test
    {
         private readonly Mock<ILogger<DepartmentController>> _Logger = new  Mock<ILogger<DepartmentController>>();  
         private readonly Mock<IUnitOfService> _unitOfService = new Mock<IUnitOfService>();
         private readonly Mock<IDepartmentService> _departmentService = new Mock<IDepartmentService>();
         private readonly Mock<IValidation> _validation = new Mock<IValidation>();
        private readonly DepartmentController _departmentController;

        // IDepartmentService service,IValidation validation, ILogger<DepartmentController> logger

        public Test()
        {
            _departmentController = new DepartmentController(_departmentService.Object,_validation.Object,_Logger.Object);
        }

        [Fact]

        public void  GetDepartments()
        {
            var Departments=DeparmentMock.DepartmentsList();
            _departmentService.Setup(obj => obj.GetDepartments()).Returns(Departments);
            var Result =_departmentController.GetDepartments() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void  CreateDepartment()
        {
            var department=DeparmentMock.Get();
            var result = new Dictionary<string,string>();
            result.Add("IsValid","true");
            _departmentService.Setup(obj => obj.CreateDepartment(department)).Returns(result);
            var Result =_departmentController.GetDepartments() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void  UpdateDepartment()
        {
            var department=DeparmentMock.Get();
            var result = new Dictionary<string,string>();
            result.Add("IsValid","true");
            _departmentService.Setup(obj => obj.UpdateDepartment(department)).Returns(result);
            var Result =_departmentController.GetDepartments() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }
        [Fact]
        public void  DisableDepartment()
        {
            var department=DeparmentMock.Get();
            var result = new Dictionary<string,string>();
            result.Add("IsValid","true");
            _departmentService.Setup(obj => obj.DisableDepartment(1,2)).Returns(true);
            var Result =_departmentController.GetDepartments() as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }

    }
}