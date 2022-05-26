using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class FeedbackService
    {
        public TraineeFeedback GetTraineeFeedbackByTrainerIdAndTraineeId(int traineeId, int trainerId)
        {
            if (traineeId == 0 || trainerId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetTraineeFeedbackByTrainerIdAndTraineeId));
            try
            {

                return _context.TraineeFeedbacks.Where(tf => tf.TraineeId == traineeId && tf.TrainerId == trainerId).Include(tf=>tf.Trainer).Include(tf=>tf.Trainee).FirstOrDefault();

            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(GetTraineeFeedbackByTrainerIdAndTraineeId));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(GetTraineeFeedbackByTrainerIdAndTraineeId));
                throw;
            }
        }
        public Dictionary<string,string> CreateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateTraineeFeedback), nameof(traineeFeedback));
            var validation = Validation.ValidateTraineeFeedback(traineeFeedback,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    CreateAndSaveTraineeFeedback(traineeFeedback);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(CreateTraineeFeedback));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(CreateTraineeFeedback));
                    throw;
                }
            }
            return validation;
        }
        public Dictionary<string,string> UpdateTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            if (traineeFeedback == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateTraineeFeedback), nameof(traineeFeedback));
            var validation = Validation.ValidateTraineeFeedback(traineeFeedback,_context);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbTraineeFeedback = _context.TraineeFeedbacks.Where(c=>c.Id==traineeFeedback.Id).FirstOrDefault();
                    if (dbTraineeFeedback != null)
                    {
                        SetUpTraineeFeedbackDetails(traineeFeedback, dbTraineeFeedback);
                        UpdateAndSaveTraineeFeedback(dbTraineeFeedback);
                    }
                    else{
                        validation.Add("Invalid Id","Not Found");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(FeedbackService), nameof(UpdateTraineeFeedback));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(FeedbackService), nameof(UpdateTraineeFeedback));
                    throw;
                }
            }
            return validation;
        }
        private void UpdateAndSaveTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            _context.TraineeFeedbacks.Update(traineeFeedback);
            _context.SaveChanges();
        }
        private void CreateAndSaveTraineeFeedback(TraineeFeedback traineeFeedback)
        {
            traineeFeedback.StatusId=1;
            traineeFeedback.CreatedOn = DateTime.UtcNow;
            _context.TraineeFeedbacks.Add(traineeFeedback);
            _context.SaveChanges();
        }
        private void SetUpTraineeFeedbackDetails(TraineeFeedback traineeFeedback,TraineeFeedback dbTraineeFeedback)
        {
            dbTraineeFeedback.StatusId=1;
            dbTraineeFeedback.Feedback = traineeFeedback.Feedback;
            dbTraineeFeedback.UpdatedOn = DateTime.UtcNow;
        }    

    }
}