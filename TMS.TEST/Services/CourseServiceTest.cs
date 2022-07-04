using System.Collections.Generic;
using TMS.API.Services;
using Moq;
using TMS.API.Controllers;
using Xunit;
using TMS.BAL;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
namespace TMS.TEST.Services
{
    public class CourseServiceTest
    {
       private readonly Mock<IUnitOfWork> _unitOfWork = new();

       private readonly ICourseService _courseService;

       private readonly List<Course> _Courses;

       private readonly Course _course;
        private readonly List<Topic> _topics;
        private readonly Topic _topic;
        private readonly User _User;
        private readonly Dictionary<string,string> result = new();
        private int courseId;
        private int topicId;
        private object topic;
        private int userId;
        private int currentUserId;
        private int departmentId;
        private int id;
         private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
        public CourseServiceTest()
       {
         _courseService = new CourseService(_unitOfWork.Object);
            _Courses = CourseMock.GetCourses();
            _course = CourseMock.GetCourse();
            _topics = TopicMock.GetTopics();
            _topic = TopicMock.GetTopic();
       } 
       [Fact]
       public void GetTopicsByCourseId()
       {
         _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
         _unitOfWork.Setup(obj => obj.Courses.GetTopicsByCourseId(courseId)).Returns(_topics);
       
       var result = _courseService.GetTopicsByCourseId(courseId);
       Assert.Equal(_topics,result);
    }
    [Fact]
    public void GetTopicByIds()
    {
   _unitOfWork.Setup(obj => obj.Validation.TopicExists(topicId)).Returns(true);
   _unitOfWork.Setup(obj => obj.Courses.GetTopicById(courseId,topicId,userId)).Returns(_topic);
   var result = _courseService.GetTopicById(courseId,topicId,userId);
   Assert.Equal(_topic,result);
    }
    [Fact]
    public void CreateTopic()
    {
      AddIsValid();
     _unitOfWork.Setup(obj => obj.Validation.ValidateTopic(_topic)).Returns(result);
     _unitOfWork.Setup(obj => obj.Courses.CreateTopic(_topic));
     _unitOfWork.Setup(obj => obj.Complete());
     var Result = _courseService.CreateTopic(_topic);
     Assert.Equal(result,Result);
    }
    [Fact]
    public void UpdateTopic()
    {
     AddIsValid();
     _unitOfWork.Setup(obj => obj.Validation.ValidateTopic(_topic)).Returns(result);
     _unitOfWork.Setup(obj => obj.Courses.UpdateTopic(_topic));
     _unitOfWork.Setup(obj => obj.Complete());
     var Result = _courseService.UpdateTopic(_topic);
     Assert.Equal(result,Result); 
    }
    [Fact]
    public void DisableTopic()
    {
      _unitOfWork.Setup(obj => obj.Validation.TopicExists(topicId, courseId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetTopicById(courseId, topicId)).Returns(_topic);
      _unitOfWork.Setup(obj => obj.Complete());
      var Result = _courseService.DisableTopic(courseId, topicId,userId);
      Assert.Equal(true,Result);

    }
    [Fact]
    public void GetCoursesByUserId()
    {
      _unitOfWork.Setup(obj => obj.Validation.UserExists(courseId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetCoursesByUserId(userId)).Returns(_Courses);
      var Result = _courseService.GetCoursesByUserId(userId);
      Assert.Equal(_Courses,Result);

    }
    [Fact]
    public void GetCoursesByDepartmentId()
    {
      _unitOfWork.Setup(obj => obj.Validation.DepartmentExists(departmentId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetCoursesByDepartmentId(departmentId)).Returns(_Courses);
      var Result = _courseService.GetCoursesByDepartmentId(departmentId);
      Assert.Equal(_Courses,Result);


    }
    [Fact]
    public void GetCourseById()
    {
      _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetCourseById(courseId)).Returns(_course);
      var Result = _courseService.GetCourseById(courseId);
      Assert.Equal(_course,Result);
    }
    [Fact]
    public void CreateCourse()
    {
      AddIsValid();
      _unitOfWork.Setup(obj => obj.Validation.ValidateCourse(_course)).Returns(result);
      _unitOfWork.Setup(obj => obj.Courses.CreateCourse(_course));
      _unitOfWork.Setup(obj => obj.Complete());
      _unitOfWork.Setup(obj => obj.Validation.UserExists(id)).Returns(true);
      _unitOfWork.Setup(obj => obj.Users.GetUserById(id)).Returns(_User);
      var Result = _courseService.CreateCourse(_course);
      Assert.Equal(result,Result);
    

    }
    [Fact]
    public void UpdateCourse()
    {
      AddIsValid();
      _unitOfWork.Setup(obj => obj.Validation.ValidateCourse(_course)).Returns(result);
      _unitOfWork.Setup(obj => obj.Courses.UpdateCourse(_course));
      _unitOfWork.Setup(obj => obj.Complete());
      _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetCourseById(courseId)).Returns(_course);
      var Result = _courseService.UpdateCourse(_course);
      Assert.Equal(result,Result);
    }
    [Fact]
    public void DisableCourse()
    {
      _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
      _unitOfWork.Setup(obj => obj.Courses.GetCourseById(courseId)).Returns(_course);
      _unitOfWork.Setup(obj => obj.Complete());
      var Result = _courseService.DisableCourse(courseId, currentUserId);
      Assert.Equal(true,Result);

    }
  
    
    }
}

