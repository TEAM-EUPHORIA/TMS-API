using Xunit;
using System;
using System.Linq;
using FluentAssertions;
using TMS.API.Controllers;
using TMS.API.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TMS.API;
using Microsoft.EntityFrameworkCore;
using UnitTesting.MockData;

namespace UnitTesting;

    public class CourseFeedbackTest{
      private static ILogger<FeedBackController> _logger;
    private static FeedbackService _feedbackService = new FeedbackService();
    private static DbContextOptionsBuilder<AppDbContext> db = AppDbContext.dbContextOptions();
    private static AppDbContext _context = new AppDbContext(db.Options);
    private readonly FeedBackController _controller = new FeedBackController(_logger,_feedbackService,_context);
     
        [Theory]
        [InlineData(1,13)]
        public void GetCourseFeedbackByCourseIdAndTraineeId_ShouldReturnStatusCode200_WithProperCourseIdandtraineeId(int courseId,int traineeId)
        {
            var result=_controller.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
            Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData(5,6)]
        [InlineData(0,0)]
        [InlineData(1,0)]
        public void GetCourseFeedbackByCourseIdAndTraineeId_ShouldReturnStatusCode404_WhenCourseIdandtraineeIdIsEmpty(int courseId,int traineeId)
        {
            var result=_controller.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
            Assert.IsType<NotFoundResult>(result);
        }
        // [Fact]
        // public void CreateCourseFeedback_ShouldReturnsStatusCode200(){
            
        //     var result=_controller.CreateCourseFeedback();
        //     Assert.IsType<OkObjectResult>(result);
        //     //result.StatusCode.Should().Be(500);
        // }
       
    
    }
