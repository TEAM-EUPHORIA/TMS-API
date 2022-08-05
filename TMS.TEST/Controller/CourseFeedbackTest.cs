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
    public class CourseFeedbackControllerTest
    {
        private readonly Mock<ILogger<FeedBackController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
          private readonly Mock<IUnitOfService> _unitofService = new();

        private readonly FeedBackController _feedbackController;
        private readonly Dictionary<string,string> result = new();
        readonly List<CourseFeedback>CourseFeedbacks= CourseFeedbackMock.GetCourseFeedbacks();
        readonly CourseFeedback CourseFeedback =CourseFeedbackMock.GetCourseFeedback();

        private void AddIsValid()
        {
            result.Add("IsValid", "true");
        }
        private void AddExists()
        {
            result.Add("Exists", "true");
        }
         int courseId = 1;
         int traineeId=1;
        public CourseFeedbackControllerTest(IStatistics stats)
        {
            _feedbackController = new FeedBackController(_unitOfService.Object, _Logger.Object, stats);

            // Arrange
            _unitOfService.Setup(obj => obj.Validation.CourseFeedbackExists(CourseFeedback.CourseId,CourseFeedback.TraineeId)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateCourseFeedback(CourseFeedback)).Returns(result);
             
            _unitOfService.Setup(obj => obj.FeedbackService.CreateCourseFeedback(CourseFeedback,1)).Returns(result);
            _unitOfService.Setup(obj => obj.FeedbackService.UpdateCourseFeedback(CourseFeedback,1)).Returns(result);
            _unitOfService.Setup(obj => obj.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId)).Returns(CourseFeedback);
          
         

        }
        [Fact]
        public void GetCourseFeedbackByCourseIdAndTraineeId_Return200Status()
        {
            //AddIsValid();
            // Act
            var Result = _feedbackController.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
        // [Fact]
        // public void GetCourseFeedbackByCourseIdAndTraineeId_500ReturnStatus()
        // {
        //     _unitOfService.Setup(obj=> obj.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId)).Throws(new InvalidOperationException());
        //     // Act        
        //     AddExists();
           
        //     var Result = _feedbackController.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId) as ObjectResult;
        //     // Assert
        //     Assert.Equal(500, Result?.StatusCode);
        // }
        [Fact]
        public void GetCourseFeedbackByCourseIdAndTraineeId_Return500Status()
        {
            _unitOfService.Setup(obj => obj.FeedbackService.GetCourseFeedbackByCourseIdAndTraineeId(courseId, traineeId)).Throws(new InvalidOperationException());
            // Act
            var Result = _feedbackController.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);
        }
          [Theory] 
          [InlineData(6,7)]
        
        public void GetCourseFeedbackByCourseIdAndTraineeId_Return404Status(int courseId,int traineeId)
        {
            // AddExists();
             _unitOfService.Setup(obj => obj.Validation.CourseFeedbackExists(courseId,traineeId)).Returns(false);
            // Act
            var Result = _feedbackController.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId) as ObjectResult;
            //    Assert
            Assert.Equal(404,Result?.StatusCode);
        }

    
        [Fact]
        public void CreateCourseFeedback_Return200Status()
        {
            AddIsValid();
            // Act
            var Result = _feedbackController.CreateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
         [Fact]
        public void CreateCourseFeedback_Return500Status()
        {
            // Arrange
            _unitOfService.Setup(obj => obj.Validation.ValidateCourseFeedback(CourseFeedback)).Throws(new InvalidOperationException());
            // Act
            var Result = _feedbackController.CreateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

        }
         [Fact]
        public void CreateCourseFeedback_Return400Status_AddExists()
        {
            // AddExists();
            _unitofService.Setup(obj => obj.FeedbackService. CreateCourseFeedback(CourseFeedback,1)).Returns(result);
            // Act
            var Result = _feedbackController.CreateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void CreateCourseFeedback_Return400Status_IsValid()
        {
            //  AddIsValid();
         _unitofService.Setup(obj => obj.FeedbackService.CreateCourseFeedback(CourseFeedback,1)).Returns(result);
            // Act
            var Result = _feedbackController.CreateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }
        
       


        [Fact]
        public void UpdateCourseFeedback_Return200Status()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
      
        [Fact]
        
        public void UpdateCourseFeedback_Return500Status()
        {
            // Arrange
            _unitOfService.Setup(obj => obj.Validation.ValidateCourseFeedback(CourseFeedback)).Throws(new InvalidOperationException());
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

        }
        [Fact]
        public void UpdateCourseFeedback_Return400Status_AddExists()
        {
            // AddExists();
            _unitofService.Setup(obj => obj.FeedbackService. UpdateCourseFeedback(CourseFeedback,1)).Returns(result);
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void UpdateCourseFeedback_Return400Status_IsValid()
        {
            //  AddIsValid();
         _unitofService.Setup(obj => obj.FeedbackService.UpdateCourseFeedback(CourseFeedback,1)).Returns(result);
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

         [Fact] 
        
        public void UpdateCourseFeedback_Return404Status()
        {
             //AddExists();
            _unitOfService.Setup(obj => obj.Validation.CourseFeedbackExists(CourseFeedback.CourseId,CourseFeedback.TraineeId)).Returns(false);
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            //    Assert
            Assert.Equal(404,Result?.StatusCode);
        }

       
        
        
    }
}