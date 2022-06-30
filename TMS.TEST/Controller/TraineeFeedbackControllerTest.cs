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
    public class TraineeFeedbackControllerTest
    {
        private readonly Mock<ILogger<FeedBackController>> _Logger = new();
        private readonly Mock<IUnitOfService> _unitOfService = new();
          private readonly Mock<IUnitOfService> _unitofService = new();

        private readonly FeedBackController _feedbackController;
        private readonly Dictionary<string,string> result = new();
        readonly List<TraineeFeedback>TraineeFeedbacks= TraineeFeedbackMock.GetTraineeFeedbacks();
        readonly TraineeFeedback TraineeFeedback =TraineeFeedbackMock.GetTraineeFeedback();

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
         int trainerId=1;
        public TraineeFeedbackControllerTest()
        {
            _feedbackController = new FeedBackController(_unitOfService.Object, _Logger.Object);

            // Arrange
            _unitOfService.Setup(obj => obj.Validation.TraineeFeedbackExists(TraineeFeedback.CourseId,TraineeFeedback.TraineeId,TraineeFeedback.TrainerId)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateTraineeFeedback(TraineeFeedback)).Returns(result);

            _unitOfService.Setup(obj => obj.FeedbackService.CreateTraineeFeedback(TraineeFeedback)).Returns(result);
            _unitOfService.Setup(obj => obj.FeedbackService.UpdateTraineeFeedback(TraineeFeedback)).Returns(result);
         _unitOfService.Setup(obj => obj.FeedbackService.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId)).Returns(TraineeFeedback);

         }
         [Fact]
        public void GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId_Return200Status()
        {
           
            // Act
           _unitOfService.Setup(obj => obj.Validation.TraineeFeedbackExists(courseId,traineeId,trainerId)).Returns(true);
            var Result = _feedbackController.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
       
          [Theory] 
          [InlineData(6,7,8)]
            public void GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId_Return404Status(int courseId,int traineeId,int trainerId)
        {
            // AddExists();
             _unitOfService.Setup(obj => obj.Validation.TraineeFeedbackExists(courseId,traineeId,trainerId)).Returns(false);
            // Act
            var Result = _feedbackController.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId,traineeId,trainerId) as ObjectResult;
            //    Assert
            Assert.Equal(404,Result?.StatusCode);
        }
    [Fact]
        public void CreateTraineeFeedback()
        {
            AddIsValid();
            // Act
            var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
    
    
        // [Fact]
        // public void CreateTraineeFeedback()
        // {
        //     AddIsValid();
        //     // Act
        //     var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
        //     // Assert
        //     Assert.Equal(200, Result?.StatusCode);
        // }
         [Fact]
        public void CreateTraineeFeedback_Return500Status()
        {
            // Arrange
<<<<<<< HEAD
      _unitOfService.Setup(obj => obj.Validation.ValidateTraineeFeedback(TraineeFeedback)).Throws(new InvalidOperationException());
=======
            _unitofService.Setup(obj => obj.FeedbackService.CreateTraineeFeedback(TraineeFeedback)).Throws(new InvalidOperationException());
>>>>>>> fd1e2fbed4a8664c1c10f0ab9b406c9612f34e19
            // Act
            var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

        }
         [Fact]
        public void CreateTraineeFeedback_Return400Status_AddExists()
        {
            // AddExists();
            _unitofService.Setup(obj => obj.FeedbackService. CreateTraineeFeedback(TraineeFeedback)).Returns(result);
            // Act
            var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void CreateTraineeFeedback_Return400Status_IsValid()
        {
            //  AddIsValid();
         _unitofService.Setup(obj => obj.FeedbackService.CreateTraineeFeedback(TraineeFeedback)).Returns(result);
            // Act
            var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void UpdateTraineeFeedback()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _feedbackController.UpdateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }
         [Fact]
        
        public void UpdateTraineeFeedback_Return500Status()
        {
            // Arrange
            _unitOfService.Setup(obj => obj.Validation.ValidateTraineeFeedback(TraineeFeedback)).Throws(new InvalidOperationException());
            // Act
            var Result = _feedbackController.UpdateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

        }
         [Fact]
        public void UpdateTraineeFeedback_Return400Status_AddExists()
        {
            // AddExists();
            _unitofService.Setup(obj => obj.FeedbackService. UpdateTraineeFeedback(TraineeFeedback)).Returns(result);
            // Act
            var Result = _feedbackController.UpdateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }

        [Fact]
        public void UpdateTraineeFeedback_Return400Status_IsValid()
        {
            //  AddIsValid();
         _unitofService.Setup(obj => obj.FeedbackService.UpdateTraineeFeedback(TraineeFeedback)).Returns(result);
            // Act
            var Result = _feedbackController.UpdateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(400, Result?.StatusCode);
        }
        //  [Fact]
        // public void UpdateTraineeFeedback_Return404Status()
        // {
        //    // AddIsValid();
        //    // AddExists();
        //     _unitofService.Setup(obj => obj.Validation.TraineeFeedbackExists( courseId, traineeId, trainerId)).Returns(false);
        //     // Act
        //     var Result = _feedbackController.UpdateTraineeFeedback(TraineeFeedback) as ObjectResult;
        //     // Assert
        //     Assert.Equal(404, Result?.StatusCode);
        // }


        

        
        
    }
}