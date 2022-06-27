using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
<<<<<<< HEAD

namespace TMS.TEST.Controller
{
    public class CourseCourseControllerTest
    {
        private readonly Mock<ILogger<FeedBackController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
        private readonly FeedBackController _feedbackController;
        private readonly Dictionary<string,string> result = new();
        readonly List<CourseFeedback>CourseFeedbacks= CourseFeedbackMock.GetCourseFeedbacks();
        readonly CourseFeedback CourseFeedback =CourseFeedbackMock.GetCourseFeedback();
=======
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
>>>>>>> 5362509541748093645dbbd45fc474b33f6dfab0

        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
<<<<<<< HEAD
        public CourseCourseControllerTest()
        {
            _feedbackController = new FeedBackController(_unitOfService.Object, _Logger.Object);

            // Arrange
            _unitOfService.Setup(obj => obj.Validation.CourseFeedbackExists(CourseFeedback.CourseId,CourseFeedback.TraineeId)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateCourseFeedback(CourseFeedback)).Returns(result);

            _unitOfService.Setup(obj => obj.FeedbackService.CreateCourseFeedback(CourseFeedback)).Returns(result);
            _unitOfService.Setup(obj => obj.FeedbackService.UpdateCourseFeedback(CourseFeedback)).Returns(result);
         

        }


    
        [Fact]
        public void CreateCourseFeedback()
        {
            AddIsValid();
            // Act
            var Result = _feedbackController.CreateCourseFeedback(CourseFeedback) as ObjectResult;
=======
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
>>>>>>> 5362509541748093645dbbd45fc474b33f6dfab0
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
<<<<<<< HEAD
        public void UpdateCourseFeedback()
=======
        public void GetCourse_Return500Status()
        {
            // Arrange
            _unitofService.Setup(obj => obj.CourseService.GetCourses()).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetCourses() as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

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
        public void GetCoursesByDepartmentId_Return200Status()
        {
            // Act
            var Result = _courseController.GetCoursesByDepartmentId(departmentId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
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
        public void CreateCourse_Return200Status()
        {
            AddIsValid();
            // Act
            var Result = _courseController.CreateCourse(Course) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourse_Return200Status()
>>>>>>> 5362509541748093645dbbd45fc474b33f6dfab0
        {
            AddIsValid();
            AddExists();
            // Act
<<<<<<< HEAD
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
=======
            var Result = _courseController.UpdateCourse(Course) as ObjectResult;
>>>>>>> 5362509541748093645dbbd45fc474b33f6dfab0
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

<<<<<<< HEAD
        
        
    }
}
=======
        [Fact]
        public void DisableCourse_Return200Status()
        {
            // Act
            var Result = _courseController.DisableCourse(Course.Id) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
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
    }
}
>>>>>>> 5362509541748093645dbbd45fc474b33f6dfab0
