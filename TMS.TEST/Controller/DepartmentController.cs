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
         private readonly Mock<ILogger<DepartmentController>> _logger= new Mock<ILogger<DepartmentController>>();
        private readonly Mock<IUnitOfService> _unitOfService = new Mock<IUnitOfService>();
        private readonly Mock<DepartmentService> _departmentService = new Mock<DepartmentService>();
        private readonly DepartmentController _departmentController;

        public DepartmentControllerTest()
        {
            
            _departmentController=new DepartmentController(_unitOfService.Object, _logger.Object);
        }

         [Fact]

        public void GetAllDepartments_ShouldReturn200Status_WhenReturnsTrue()
        {
                 _departmentService.Setup(obj => obj.GetDepartments()).Returns(new List<Department>());
                 var Results =_departmentController.GetDepartments() as ObjectResult;
                 Assert.Equal(200,Results?.StatusCode);
        }

    }
}