using System.Collections.Generic;
using TMS.API.Services;
using TMS.API.Repositories;
using Moq;
using Xunit;
using TMS.BAL;
using Microsoft.Extensions.Logging;

namespace TMS.TEST.Services
{
    public class TraineeFeedbackServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly IFeedbackService _feedbackService;
        private readonly List<TraineeFeedback> _traineeFeedbacks;
        private readonly TraineeFeedback _traineeFeedback;
        private readonly Dictionary<string, string> result = new();
        private int courseId;
        private int traineeId;
        private int trainerId;

        public TraineeFeedbackServiceTest(ILogger<FeedbackService> logger)
        {
            _feedbackService = new FeedbackService(_unitOfWork.Object, logger);
            _traineeFeedbacks = TraineeFeedbackMock.GetTraineeFeedbacks();
            _traineeFeedback = TraineeFeedbackMock.GetTraineeFeedback();
        }


        // }
        [Fact]
        public void GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId()
        {
            _unitOfWork.Setup(obj => obj.Validation.CourseExists(courseId)).Returns(true);
            _unitOfWork.Setup(obj => obj.Validation.UserExists(traineeId)).Returns(true);
            _unitOfWork.Setup(obj => obj.Validation.UserExists(traineeId)).Returns(true);

            _unitOfWork.Setup(obj => obj.Feedbacks.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId, traineeId, trainerId)).Returns(_traineeFeedback);
            _unitOfWork.Setup(obj => obj.Validation.TraineeFeedbackExists(traineeId, courseId, trainerId)).Returns(true);
            var result = _feedbackService.GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(courseId, traineeId, trainerId);
            Assert.Equal(_traineeFeedback, result);
        }
        [Fact]
        public void CreateTraineeFeedback()
        {
            //   AddIsValid();
            _unitOfWork.Setup(obj => obj.Validation.ValidateTraineeFeedback(_traineeFeedback)).Returns(result);
            _unitOfWork.Setup(obj => obj.Feedbacks.CreateTraineeFeedback(_traineeFeedback));
            _unitOfWork.Setup(obj => obj.Complete());
            var Result = _feedbackService.CreateTraineeFeedback(_traineeFeedback,1);
            Assert.Equal(result, Result);
        }
        [Fact]
        public void UpdateTraineeFeedback()
        {
            //  AddIsValid();
            _unitOfWork.Setup(obj => obj.Validation.ValidateTraineeFeedback(_traineeFeedback)).Returns(result);
            _unitOfWork.Setup(obj => obj.Feedbacks.UpdateTraineeFeedback(_traineeFeedback));
            _unitOfWork.Setup(obj => obj.Complete());
            var Result = _feedbackService.UpdateTraineeFeedback(_traineeFeedback,1);
            Assert.Equal(result, Result);
        }


    }
}