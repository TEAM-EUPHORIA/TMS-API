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

namespace TMS.TEST.Controller
{
    public class DepartmentControllerTest
    {
         private readonly Mock<ILogger<DepartmentController>> _Logger = new  Mock<ILogger<DepartmentController>>();  
         private readonly Mock<IDepartmentService> _departmentService = new Mock<IDepartmentService>();
        private readonly DepartmentController _departmentController;

        // public DepartmentControllerTest()
        // {
        //     _departmentController = new DepartmentController(_departmentService.Object);
        // }

        // [Fact]

        // public void  ABC()
        // {
        //     var Departments=DeparmentMock.DepartmentsList();
        //     _departmentService.Setup(obj => obj.GetDepartments()).Returns(Departments);
        //     var Result =_departmentController.GetDepartments() as ObjectResult;
        //     Assert.Equal(200,Result?.StatusCode);
        // }

    }
}