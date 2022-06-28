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
        public CourseFeedbackControllerTest()
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
        public void UpdateCourseFeedback()
        {
            AddIsValid();
            AddExists();
            // Act
            var Result = _feedbackController.UpdateCourseFeedback(CourseFeedback) as ObjectResult;
            // Assert
            Assert.Equal(200, Result?.StatusCode);
        }

        
        
    }
}