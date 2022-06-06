using Xunit;
using FluentAssertions;
using TMS.API.Controllers;
using TMS.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.API;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace TMS.TEST;

public class UnitTest1
{
   
    // private static ILogger<FeedBackController> _logger;
    // private static FeedbackService _feedbackService = new FeedbackService();
    // private static DbContextOptionsBuilder<AppDbContext> db = AppDbContext.dbContextOptions();
    // private static AppDbContext _context = new AppDbContext(db.Options);
    // private readonly FeedBackController _controller = new FeedBackController(_logger,_feedbackService,_context);
     
    //     [Theory]
    //     [InlineData(1,13)]
    //     public void GetCourseFeedbackByCourseIdAndTraineeId_ShouldReturnStatusCode400_WhenLocationNameIsEmpty(int courseId,int traineeId)
    //     {
    //         var result=_controller.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId)as  ObjectResult;
    //         //_controller.Setup(r=>r.GetCourseFeedbackByCourseIdAndTraineeId(courseId,traineeId,_context.Object)).Returns(true);
    //         result.StatusCode.Should().Be(200);
    //     }
    
}