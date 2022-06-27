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
        private readonly UserController _UserController;

        private readonly Dictionary<string, string> result = new();
        readonly List<Course> Courses = CourseMock.GetCourses();
        readonly Course Course = CourseMock.GetCourse();
        readonly List<Topic> topics = TopicMock.GetTopics();
        readonly Topic Topic = TopicMock.GetTopic();
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

            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId, Topic.CourseId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.CourseExists(Topic.CourseId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.ValidateTopic(Topic)).Returns(result);

            _unitofService.Setup(obj => obj.CourseService.GetTopicsByCourseId(Topic.CourseId)).Returns(topics);
            _unitofService.Setup(obj => obj.CourseService.GetTopicById(Topic.CourseId, Topic.TopicId, userId)).Returns(Topic);
            _unitofService.Setup(obj => obj.CourseService.CreateTopic(Topic)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.UpdateTopic(Topic)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.DisableTopic(Topic.CourseId, Topic.TopicId, 1)).Returns(true);

        }

        [Fact]
        public void GetCourses_Return200Status()
        {
            // Act
            var Result = _courseController.GetCourses() as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCourses_Return500Status()
        {
            // Arrange
            _unitofService.Setup(obj => obj.CourseService.GetCourses()).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetCourses() as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

        }

         [Fact]
        public void GetCoursesByUserId_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(false);
            // Act
            var Result = _courseController.GetCourseById(courseId) as ObjectResult;
            //    Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetCoursesByUserId_Return200Status()
        {
            // Act
            var Result = _courseController.GetCoursesByUserId(userId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCoursesByUserId_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetCoursesByUserId(userId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetCoursesByUserId(userId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

         [Fact]
        public void GetCoursesByDepartmentId_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.DepartmentExists(departmentId)).Returns(false);
            // Act
            var Result = _courseController.GetCoursesByDepartmentId(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetCoursesByDepartmentId_Return200Status()
        {
            // Act
            var Result = _courseController.GetCoursesByDepartmentId(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCoursesByDepartmentId_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetCoursesByDepartmentId(departmentId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetCoursesByDepartmentId(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void GetCourseById_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.CourseExists(Course.Id)).Returns(false);
            // Act
            var Result = _courseController.GetCourseById(courseId) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetCourseById_Return200Status()
        {
            // Act
            var Result = _courseController.GetCourseById(courseId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetCourseById_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetCourseById(courseId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetCourseById(courseId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }


        [Fact]
        public void CreateCourse_Return400Status_AddExists()
        {
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.CreateCourse(Course)).Returns(result);
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void CreateCourse_Return400Status_IsValid()
        {
            _unitofService.Setup(obj => obj.CourseService.CreateCourse(Course)).Returns(result);
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void CreateCourse_Return200Status()
        {
            AddIsValid();
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void CreateCourse_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.CreateCourse(Course)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse_Return200Status()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse_Return500Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateCourse(Course)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(false);
            // Act
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse_Return400Status()
        {
            //AddIsValid();
            // AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateCourse(Course)).Returns(result);
            // Act
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void DisableCourse_Return200Status()
        {
            // Act
            var Result = _courseController.DisableCourse(courseId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void DisableCourse_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.DisableCourse(courseId, userId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.DisableCourse(Course.Id) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void DisableCourse_Return404Status()
        {
            // _unitofService.Setup(obj => obj.CourseService.UpdateCourse(Course)).Returns(result);
            _unitofService.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(false);
            // Act
            var Result = _courseController.DisableCourse(courseId) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void GetTopicsByCourseId_Return200Status()
        {
            var Result = _courseController.GetTopicsByCourseId(Topic.CourseId) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void GetTopicByIds_Return200Status()
        {
            AddIsValid();
            var Result = _courseController.GetTopicByIds(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void CreateTopic_Return200Status()
        {
            AddIsValid();
            var Result = _courseController.CreateTopic(Topic) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void UpdateTopic_Return200Status()
        {
            AddIsValid();
            AddExists();
            var Result = _courseController.UpdateTopic(Topic) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void DisableTopic_Return200Status()
        {
            var Result = _courseController.DisableTopic(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]

        public void GetTopicsByCourseId_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetTopicsByCourseId(Topic.CourseId)).Throws(new InvalidOperationException());
            var Result = _courseController.GetTopicsByCourseId(Topic.CourseId) as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);

        }
        [Fact]
        public void GetTopicByIds_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.GetTopicById(Topic.CourseId, Topic.TopicId, userId)).Throws(new InvalidOperationException());
            var Result = _courseController.GetTopicByIds(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);

        }
        [Fact]
        public void CreateTopic_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.CreateTopic(Topic)).Throws(new InvalidOperationException());
            var Result = _courseController.CreateTopic(Topic) as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void UpdateTopic_Return500Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateTopic(Topic)).Throws(new InvalidOperationException());
            var Result = _courseController.UpdateTopic(Topic) as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void DisableTopic_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.DisableTopic(Topic.CourseId, Topic.TopicId, 1)).Throws(new InvalidOperationException());
            var Result = _courseController.DisableTopic(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]

        public void GetTopicsByCourseId_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.CourseExists(Topic.CourseId)).Returns(false);
            var Result = _courseController.GetTopicsByCourseId(Topic.CourseId) as ObjectResult;
            Assert.Equal(404, Result?.StatusCode);

        }
        [Fact]
        public void GetTopicByIds_Return404Status()
        {
            AddExists();
            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId, Topic.CourseId)).Returns(false);
            var Result = _courseController.GetTopicByIds(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(404, Result?.StatusCode);
        }
        [Fact]
        public void CreateTopic_Return400tatus_IsValid()
        {

            _unitofService.Setup(obj => obj.CourseService.CreateTopic(Topic)).Returns(result);
            var Result = _courseController.CreateTopic(Topic) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateTopic_Return400tatus_Exists()
        {
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.CreateTopic(Topic)).Returns(result);
            var Result = _courseController.CreateTopic(Topic) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);



        }
        [Fact]
        public void UpdateTopic_Return400Status_IsValid()
        {

            _unitofService.Setup(obj => obj.CourseService.UpdateTopic(Topic)).Returns(result);
            var Result = _courseController.UpdateTopic(Topic) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void UpdateTopic_Return400Status_Exists()
        {
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateTopic(Topic)).Returns(result);
            var Result = _courseController.UpdateTopic(Topic) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void UpdateTopic_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId, Topic.CourseId)).Returns(false);
            var Result = _courseController.UpdateTopic(Topic) as ObjectResult;
            Assert.Equal(404, Result?.StatusCode);
        }
        [Fact]
        public void DisableTopic_Return404Status()
        {
            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId, Topic.CourseId)).Returns(false);
            var Result = _courseController.DisableTopic(Topic.CourseId, Topic.TopicId) as ObjectResult;
            Assert.Equal(404, Result?.StatusCode);
        }

    }
}
