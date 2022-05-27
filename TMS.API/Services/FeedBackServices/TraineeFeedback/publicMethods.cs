using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        public TraineeFeedback GetTraineeFeedbackByCourseIdTrainerIdAndTraineeId(int courseId,int traineeId, int trainerId,AppDbContext dbContext)
        {   
            var courseExists = Validation.CourseExists(dbContext,courseId);
            var traineeExists = Validation.UserExists(dbContext,traineeId);
            var trainerExists = Validation.UserExists(dbContext,trainerId);
            if(courseExists && traineeExists && trainerExists)
            {
                var traineeFeedbackExists = Validation.TraineeFeedbackExists(dbContext,courseId,traineeId,trainerId);        
                if(traineeFeedbackExists)
                {
                    var result = dbContext.TraineeFeedbacks.Where(tf =>tf.CourseId==courseId && tf.TraineeId == traineeId && tf.TrainerId == trainerId).Include(tf=>tf.Trainer).Include(tf=>tf.Trainee).FirstOrDefault();          
                    if(result is not null) return result;
                }
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string,string> CreateTraineeFeedback(TraineeFeedback traineeFeedback,AppDbContext dbContext)
        {
            if (traineeFeedback is null) throw new ArgumentNullException(nameof(traineeFeedback));
            var validation = Validation.ValidateTraineeFeedback(traineeFeedback,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                CreateAndSaveTraineeFeedback(traineeFeedback,dbContext);
            }
            return validation;
        }
        public Dictionary<string,string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback,AppDbContext dbContext)
        {
            if (traineeFeedback is null) throw new ArgumentNullException(nameof(traineeFeedback));
            var validation = Validation.ValidateTraineeFeedback(traineeFeedback,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbTraineeFeedback = dbContext.TraineeFeedbacks.Find(traineeFeedback.CourseId,traineeFeedback.TraineeId,traineeFeedback.TrainerId);
                if(dbTraineeFeedback is not null)
                {
                    SetUpTraineeFeedbackDetails(traineeFeedback, dbTraineeFeedback);
                    UpdateAndSaveTraineeFeedback(dbTraineeFeedback, dbContext);
                }
            }
            return validation;
        }
    }
}