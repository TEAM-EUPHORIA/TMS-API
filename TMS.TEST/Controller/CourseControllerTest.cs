using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using System;
using TMS.API.ViewModels;

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
        readonly List<Attendance> attendances = AttendanceMock.GetAttendances();
        readonly Attendance attendance = AttendanceMock.GetAttendance();
        readonly AddUsersToCourse data = CourseMock.GetData();
        Dictionary<string, List<CourseUsers>> _result = new Dictionary<string, List<CourseUsers>>();
        int userId = 1;
        int departmentId = 1;
        int courseId = 1;
        Assignment assignment = AssignmentMock.GetAssignment();
        int topicId = 3;
        int ownerId = 1;
        int currentUserId = 1;

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
            _result.Add("hello", CourseMock.GetResult());
            _courseController = new CourseController(_unitofService.Object, _Logger.Object);

            // Arrange Course
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

            // Arrange Topic
            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId, Topic.CourseId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.CourseExists(Topic.CourseId)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.ValidateTopic(Topic)).Returns(result);
            _unitofService.Setup(obj => obj.Validation.TopicExists(Topic.TopicId)).Returns(true);


            _unitofService.Setup(obj => obj.CourseService.GetTopicsByCourseId(Topic.CourseId)).Returns(topics);
            _unitofService.Setup(obj => obj.CourseService.GetTopicById(Topic.CourseId, Topic.TopicId, userId)).Returns(Topic);
            _unitofService.Setup(obj => obj.CourseService.CreateTopic(Topic)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.UpdateTopic(Topic)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.DisableTopic(Topic.CourseId, Topic.TopicId, 1)).Returns(true);
            

            // Arrange 
            _unitofService.Setup(obj => obj.Validation.AttendanceExists(3, 3, 3)).Returns(true);
            _unitofService.Setup(obj => obj.Validation.ValidateAttendance(attendance)).Returns(result);
            _unitofService.Setup(obj => obj.Validation.ValidateAssignment(assignment)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.GetAssignmentByCourseIdTopicIdAndOwnerId(1, 3, 1)).Returns(assignment);
            _unitofService.Setup(obj => obj.Validation.AssignmentExists(1, 3, 1)).Returns(true);
            _unitofService.Setup(obj => obj.CourseService.MarkAttendance(attendance)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.AddUsersToCourse(data, userId)).Returns(_result);
            _unitofService.Setup(obj => obj.CourseService.RemoveUsersFromCourse(data, userId)).Returns(_result);
            _unitofService.Setup(obj => obj.CourseService.GetAssignmentsByTopicId(topicId)).Returns(Topic.Assignments);
            _unitofService.Setup(obj => obj.CourseService.CreateAssignment(assignment)).Returns(result);
            _unitofService.Setup(obj => obj.CourseService.UpdateAssignment(assignment)).Returns(result);

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
        [Fact]
        public void Attendance_Return200Status()
        {
            AddIsValid();
            // AddExists();
            // Act
            var Result = _courseController.MarkAttendance(attendance) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
         [Fact]
        public void Attendance_Return400Status()
        {
            // Arrange
            _unitofService.Setup(obj => obj.CourseService.MarkAttendance(attendance)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.MarkAttendance(attendance) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);

        }
         [Fact]
        public void Attendance_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.MarkAttendance(attendance)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.MarkAttendance(attendance) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }
        
        

        [Fact]
        public void AssignUser_Return200Status()
        {
            var Result = _courseController.AssignUsersToCourse(data) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
         [Fact]
        public void AssignUser_Return500Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.AddUsersToCourse(data, currentUserId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.AssignUsersToCourse(data) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }
        

         [Fact]
        public void AssignUser_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.AssignmentExists(courseId, topicId, ownerId)).Returns(false);
            // Act
            var Result = _courseController.UpdateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void RemoveUser_Return200Status()
        {
            var Result = _courseController.RemoveUsersFromCourse(data) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
         [Fact]
        public void RemoveUser_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.RemoveUsersFromCourse(data, currentUserId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.RemoveUsersFromCourse(data) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }
         [Fact]
        public void RemoveUser_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(false);
            // Act
            var Result = _courseController.RemoveUsersFromCourse(data) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

    
        [Fact]
        public void GetAssignmentsByTopicId_Return200Status()
        {
            var Result = _courseController.GetAssignmentsByTopicId(courseId, topicId) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }

       [Fact]
        public void GetAssignmentsByTopicId_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.TopicExists(topicId, courseId)).Returns(false);
            // Act
            var Result = _courseController.GetAssignmentsByTopicId(courseId, topicId) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }


        [Fact]
        public void GetAssignmentsByTopicId_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetAssignmentsByTopicId(topicId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetAssignmentsByTopicId(courseId, topicId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }


        [Fact]
        public void GetAssignmentByCourseIdTopicIdAndOwnerId_Return200Status()
        {
            AddIsValid();
            var Result = _courseController.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId) as ObjectResult;
            Assert.
            Equal(200, Result?.StatusCode);
        }

          [Fact]
        public void GetAssignmentByCourseIdTopicIdAndOwnerId_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.AssignmentExists(courseId, topicId, ownerId)).Returns(false);
            // Act
            var Result = _courseController.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }
        [Fact]
        public void GetAssignmentByCourseIdTopicIdAndOwnerId_Return500Status()
        {
            _unitofService.Setup(obj => obj.CourseService.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, ownerId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        

        [Fact]
        public void CreateAssignment_Return200Status()
        {
            AddIsValid();
            var Result = _courseController.CreateAssignment(assignment) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }

         [Fact]
        public void  CreateAssignment_Return400Status_AddExists()
        {
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.CreateAssignment(assignment)).Returns(result);
            // Act
            var Result = _courseController.CreateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

       

        [Fact]
        public void CreateAssignment_Return500Status()
        {
            AddIsValid();
            _unitofService.Setup(obj => obj.CourseService.CreateAssignment(assignment)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.CreateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }


        [Fact]
        public void UpdateAssignment_Return200Status()
        {
            AddIsValid();
            AddExists();
            var Result = _courseController.UpdateAssignment(assignment) as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }

         [Fact]
        public void UpdateAssignment_Return500Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateAssignment(assignment)).Throws(new InvalidOperationException());
            // Act
            var Result = _courseController.UpdateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void UpdateAssignment_Return404Status()
        {
            AddIsValid();
            AddExists();
            _unitofService.Setup(obj => obj.Validation.AssignmentExists(courseId, topicId, ownerId)).Returns(false);
            // Act
            var Result = _courseController.UpdateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void UpdateAssignment_Return400Status()
        {
            //AddIsValid();
            // AddExists();
            _unitofService.Setup(obj => obj.CourseService.UpdateAssignment(assignment)).Returns(result);
            // Act
            var Result = _courseController.UpdateAssignment(assignment) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }


    }
}


