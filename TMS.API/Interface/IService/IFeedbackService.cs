using TMS.BAL;

namespace TMS.API.Services
{
    public interface IFeedbackService
    {
        Dictionary<string, string> CreateCourseFeedback(CourseFeedback courseFeedback, int createdBy);
        Dictionary<string, string> CreateTraineeFeedback(TraineeFeedback traineeFeedback, int updatedBy);
        CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId);
        TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId, int traineeId, int trainerId);
        Dictionary<string, string> UpdateCourseFeedback(CourseFeedback courseFeedback, int createdBy);
        Dictionary<string, string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback, int updatedBy);
    }
}