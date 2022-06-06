using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        public CourseFeedback GetCourseFeedbackByCourseIdAndTraineeId(int courseId, int traineeId,AppDbContext dbContext)
        {
            var courseExists = Validation.CourseExists(dbContext,courseId);
            var traineeExists = Validation.UserExists(dbContext,traineeId);
            if(courseExists && traineeExists)
            {
                var result = dbContext.CourseFeedbacks.Where(cf => cf.CourseId == courseId && cf.TraineeId == traineeId).Include(cf=>cf.Trainee).Include(cf=>cf.Course).FirstOrDefault();        
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id's");
        }
        public Dictionary<string,string> CreateCourseFeedback(CourseFeedback courseFeedback,AppDbContext dbContext)
        {
           if (courseFeedback is null) throw new ArgumentNullException(nameof(courseFeedback));
            var validation = Validation.ValidateCourseFeedback(courseFeedback,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))            
            {
                CreateAndSaveCourseFeedback(courseFeedback,dbContext);
            }
            return validation;
        }
        public Dictionary<string,string> UpdateCourseFeedback(CourseFeedback courseFeedback,AppDbContext dbContext)
        {
           if (courseFeedback is null) throw new ArgumentNullException(nameof(courseFeedback));
            var validation = Validation.ValidateCourseFeedback(courseFeedback,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbCourseFeedback = dbContext.CourseFeedbacks.Find(courseFeedback.CourseId,courseFeedback.TraineeId);
                if(dbCourseFeedback is not null)
                {
                    SetUpCourseFeedbackDetails(courseFeedback, dbCourseFeedback);
                    UpdateAndSaveCourseFeedback(dbCourseFeedback,dbContext);
                }
            }
            return validation;
        }
    }
}