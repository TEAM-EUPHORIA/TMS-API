using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public class ReviewService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(AppDbContext context, ILogger<ReviewService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Review> GetReviewByStatus(int statusId)
        {
            if (statusId == 0) throw new ArgumentException("GetReviewByStatus requires a vaild Id not zero");
            try
            {
                return _context.Reviews.Where(r => r.StatusId == statusId).Include("Reviewer").Include("Trainee").Include("Status").ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }



        public Object GetReviewById(int id)
        {
            var dbReview = _context.Reviews.Where(r => r.Id == id).Include("Reviewer").Include("Trainee").Include("Status").FirstOrDefault();
            if (dbReview != null)
            {

                var result = new
                {
                    Id = dbReview.Id,
                    ReviewerId = dbReview.ReviewerId,
                    StatusId = dbReview.StatusId,
                    TraineeId = dbReview.TraineeId,
                    ReviewDate = dbReview.ReviewDate,
                    ReviewTime = dbReview.ReviewTime,
                    Mode = dbReview.Mode,
                };

                return result;
            }
            return "not found";
        }


        public void CreateReview(Review review)
        {
            if (review == null) throw new ArgumentException("CreateRequire requires a vaild User Object");
            try
            {
                Random ran = new Random();
                Review dbReview = new Review();
                dbReview.ReviewerId = review.ReviewerId;
                dbReview.StatusId = review.StatusId;
                dbReview.TraineeId = review.TraineeId;
                dbReview.ReviewDate = review.ReviewDate;
                dbReview.ReviewTime = review.ReviewTime;
                dbReview.Mode = review.Mode;

                dbReview.CreatedOn = DateTime.Now;

                _context.Reviews.Add(dbReview);

                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void UpdateReview(Review review)
        {
            if (review == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
            try
            {
                var dbReview = _context.Reviews.Find(review.Id);
                if (dbReview != null)
                {
                    dbReview.ReviewerId = review.ReviewerId;
                    dbReview.StatusId = review.StatusId;
                    dbReview.TraineeId = review.TraineeId;
                    dbReview.ReviewDate = review.ReviewDate;
                    dbReview.ReviewTime = review.ReviewTime;
                    dbReview.Mode = review.Mode;

                    dbReview.UpdatedOn = DateTime.Now;

                    _context.Update(dbReview);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void DisableReview(Review review)
        {
            if (review == null) throw new ArgumentException("DisableUser requires a vaild User Object");
            try
            {
                var dbReview = _context.Reviews.Find(review.Id);
                if (dbReview != null)
                {

                    dbReview.isDisabled = true;
                    dbReview.UpdatedOn = DateTime.Now;

                    _context.Update(dbReview);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }


        public Object GetMomById(int id)
        {
            var dbMOM = _context.MOMs.Where(m => m.Id == id).Include("Review").Include("Owner").Include("Status").FirstOrDefault();
            if (dbMOM != null)
            {

                var result = new
                {
                    Id = dbMOM.Id,
                    ReviewId = dbMOM.ReviewId,
                    StatusId = dbMOM.StatusId,
                    OwnerId = dbMOM.OwnerId,
                    Agenda = dbMOM.Agenda,
                    MeetingNotes = dbMOM.MeetingNotes,
                    PurposeOfMeeting = dbMOM.PurposeOfMeeting
                };

                return result;
            }
            return "not found";
        }
        public void CreateMOM(MOM mom)
        {
            if (mom == null) throw new ArgumentException("CreateMOM requires a vaild MOM Object");
            try
            {
                Random ran = new Random();
                MOM dbMOM = new MOM();
                dbMOM.ReviewId = mom.ReviewId;
                dbMOM.StatusId = mom.StatusId;
                dbMOM.OwnerId = mom.OwnerId;
                dbMOM.Agenda = mom.Agenda;
                dbMOM.MeetingNotes = mom.MeetingNotes;
                dbMOM.PurposeOfMeeting = mom.PurposeOfMeeting;

                dbMOM.CreatedOn = DateTime.Now;
                _context.MOMs.Add(dbMOM);
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Review services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Review services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void UpdateMOM(MOM mom)
        {
            if (mom == null) throw new ArgumentException("UpdateMOM requires a vaild MOM Object");
            try
            {
                var dbMOM = _context.MOMs.Find(mom.Id);
                if (dbMOM != null)
                {
                    dbMOM.ReviewId = mom.ReviewId;
                    dbMOM.StatusId = mom.StatusId;
                    dbMOM.OwnerId = mom.OwnerId;
                    dbMOM.Agenda = mom.Agenda;
                    dbMOM.MeetingNotes = mom.MeetingNotes;
                    dbMOM.PurposeOfMeeting = mom.PurposeOfMeeting;

                    dbMOM.CreatedOn = DateTime.Now;

                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in MOM services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in MOM services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public IEnumerable<MOM> GetMOMByStatus(int statusId)
        {
            if (statusId == 0) throw new ArgumentException("GetMOMByStatus requires a vaild Id not zero");
            try
            {
                return _context.MOMs.Where(m => m.StatusId == statusId).Include("Review").Include("Owner").Include("Status").ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in MOM services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in MOM services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

    }
}