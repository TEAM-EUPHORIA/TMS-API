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
        public TraineeFeedbackControllerTest()
        {
            _feedbackController = new FeedBackController(_unitOfService.Object, _Logger.Object);

            // Arrange
            _unitOfService.Setup(obj => obj.Validation.TraineeFeedbackExists(TraineeFeedback.CourseId,TraineeFeedback.TraineeId,TraineeFeedback.TrainerId)).Returns(true);
            _unitOfService.Setup(obj => obj.Validation.ValidateTraineeFeedback(TraineeFeedback)).Returns(result);

            _unitOfService.Setup(obj => obj.FeedbackService.CreateTraineeFeedback(TraineeFeedback)).Returns(result);
            _unitOfService.Setup(obj => obj.FeedbackService.UpdateTraineeFeedback(TraineeFeedback)).Returns(result);
         

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
         [Fact]
        public void CreateTraineeFeedback_Return500Status()
        {
            // Arrange
            _unitofService.Setup(obj => obj.FeedbackService.CreateTraineeFeedback(TraineeFeedback)).Throws(new InvalidOperationException());
            // Act
            var Result = _feedbackController.CreateTraineeFeedback(TraineeFeedback) as ObjectResult;
            // Assert
            Assert.Equal(500, Result?.StatusCode);

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

        
        
    }
}