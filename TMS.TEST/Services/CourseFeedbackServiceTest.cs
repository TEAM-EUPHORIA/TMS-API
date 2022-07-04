using System.Collections.Generic;
using TMS.API.Services;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
namespace TMS.TEST.Services
{
    public class CourseFeedbackServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly IFeedbackService _feedbackService;
        private readonly List<CourseFeedback> _courseFeedbacks;
        private readonly CourseFeedback _courseFeedback;
         private readonly Dictionary<string,string> result = new();

         private int courseId;
        private int traineeId;
      
        public CourseFeedbackServiceTest()
        {
            _feedbackService = new FeedbackService(_unitOfWork.Object);
            _courseFeedbacks = CourseFeedbackMock.GetCourseFeedbacks();
            _courseFeedback = CourseFeedbackMock.GetCourseFeedback();
        }
        // [Fact]
        // public void  CreateCourseFeedback()
        // {
        //     // Arrange
        //     _unitOfWork.Setup(obj => obj.Feedbacks.CreateCourseFeedback()).Returns(_courseFeedback);
        //     // Act
        //     var result = _feedbackService.CreateCourseFeedback();
        //     // Assert
        //     Assert.Equal(_courseFeedback,result);
        // }

    
        // [Fact]
        // public void GetCourseFeedbackByCourseIdAndTraineeId()
        // {
        //     // Arrange
        //     _unitOfWork.Setup(obj => obj.Validation. CourseFeedbackExists(_courseFeedback.CourseId!,_courseFeedback.TraineeId!)).Returns(true);
        //     _unitOfWork.Setup(obj => obj.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(_courseFeedback.CourseId!,_courseFeedback.TraineeId!)).Returns(_courseFeedback);
        //     // Act
        //     var result = _feedbackService.GetCourseFeedbackByCourseIdAndTraineeId(_courseFeedback.CourseId,_courseFeedback.TraineeId);
        //     // Assert
        //     Assert.Equal(_courseFeedback,result);
        // }
          [Fact]
       public void GetCourseFeedbackByCourseIdAndTraineeId()
       {
         _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
         _unitOfWork.Setup(obj => obj.Validation.UserExists(traineeId)).Returns(true);

         _unitOfWork.Setup(obj => obj.Feedbacks.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId)).Returns(_courseFeedback);
       
       var result = _feedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
       Assert.Equal(_courseFeedback,result);
    }
             [Fact]
    public void CreateCourseFeedback()
    {
    //   AddIsValid();
     _unitOfWork.Setup(obj => obj.Validation.ValidateCourseFeedback(_courseFeedback)).Returns(result);
     _unitOfWork.Setup(obj => obj.Feedbacks.CreateCourseFeedback(_courseFeedback));
     _unitOfWork.Setup(obj => obj.Complete());
     var Result = _feedbackService.CreateCourseFeedback(_courseFeedback);
     Assert.Equal(result,Result);
    }
    [Fact]
    public void UpdateCourseFeedback()
    {
    //  AddIsValid();
     _unitOfWork.Setup(obj => obj.Validation.ValidateCourseFeedback(_courseFeedback)).Returns(result);
     _unitOfWork.Setup(obj => obj.Feedbacks.UpdateCourseFeedback(_courseFeedback));
     _unitOfWork.Setup(obj => obj.Complete());
     var Result = _feedbackService.UpdateCourseFeedback(_courseFeedback);
     Assert.Equal(result,Result); 
    }


    }
}