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
    public class CourseControllerTest
    {
        private readonly Mock<ILogger<CourseController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitofService = new();
        private readonly CourseController _courseController;
        private readonly Dictionary<string,string> result = new();
        readonly List<Course> Courses = CourseMock.GetCourses();
        readonly Course Course = CourseMock.GetCourse();
        int userId = 1;
        int departmentId = 1;
        int courseId = 1;


        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
        public CourseControllerTest()
        {
            _courseController = new CourseController(_unitofService.Object, _Logger.Object);

            // Arrange
            _unitofService.Setup(obj => obj.Validation.CourseExists(Course.Id)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.UserExists(userId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.DepartmentExists(departmentId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.ValidateCourse(Course)).Returns(result);

            _unitofService.Setup(obj => obj.CourseService.DisableCourse(Course.Id, 1)).Returns(true);
            _unitofService.Setup(obj => obj.CourseService.GetCourses()).Returns(Courses);
            _unitofService.Setup(obj => obj.CourseService.GetCoursesByUserId(userId)).Returns(Courses);
            _unitofService.Setup(obj => obj.CourseService.GetCoursesByDepartmentId(departmentId)).Returns(Courses);
            _unitofService.Setup(obj => obj.CourseService.GetCourseById(courseId)).Returns(Course);
            _unitofService.Setup(obj => obj.CourseService.CreateCourse(Course)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.UpdateCourse(Course)).Returns(result);

            _unitofService.Setup(obj => obj.CourseService.GetCourses()).Throws(new InvalidOperationException());


        }

        [Fact]
        public void GetCourses()
        {
            // Act
            var Result = _courseController.GetCourses() as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
         public void GetCourse_Return500Status()
         {
            // Act
            var Result= _courseController.GetCourses() as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

         }

        [Fact]
        public void GetCoursesByUserId()
        {
            // Act
            var Result = _courseController.GetCoursesByUserId(userId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCoursesByDepartmentId()
        {
            // Act
            var Result = _courseController.GetCoursesByDepartmentId(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCourseById()
        {
            // Act
            var Result = _courseController.GetCourseById(courseId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void CreateCourse()
        {
            AddIsValid();
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(200,Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void DisableCourse()
        {
            // Act
            var Result = _courseController.DisableCourse(Course.Id) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
    }
}