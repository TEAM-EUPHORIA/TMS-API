using Xunit;
using FluentAssertions;
using TMS.API.Controllers;
using TMS.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


using Moq;
using TMS.API;
using Microsoft.EntityFrameworkCore;

namespace TMS.TEST.Controller;

    public class CourseFeedbackTest{
      private static ILogger<FeedBackController> _logger;
    private static FeedbackService _feedbackService = new FeedbackService();
    private static DbContextOptionsBuilder<AppDbContext> db = AppDbContext.dbContextOptions();
    private static AppDbContext _context = new AppDbContext(db.Options);
    private readonly FeedBackController _controller = new FeedBackController(_logger,_feedbackService,_context);
     
        [Theory]
        [InlineData(1,13)]
        public void GetCourseFeedbackByCourseIdAndTraineeId_ShouldReturnStatusCode400_WhenCourseIdandtraineeIdIsEmpty(int courseId,int traineeId)
        {
            var result=_controller.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
            Assert.IsType<OkObjectResult>(result);
        }
        [Theory]
        [InlineData(5,6)]
        public void GetCourseFeedbackByCourseIdAndTraineeId_ShouldReturnStatusCode404_WhenCourseIdandtraineeIdIsEmpty(int courseId,int traineeId)
        {
            var result=_controller.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId);
            Assert.IsType<NotFoundResult>(result);
        }
    
    }
