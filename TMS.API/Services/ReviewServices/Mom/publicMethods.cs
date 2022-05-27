using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class ReviewService
    {
        public IEnumerable<MOM> GetListOfMomByUserId(int userId,AppDbContext dbContext)
        {
            var userExists = Validation.UserExists(dbContext,userId);
            if(userExists) return dbContext.MOMs.Where(m=>m.TraineeId==userId).Include(m=>m.Review).Include(m=>m.Trainee); 
            else throw new ArgumentException("Invalid Id");          
        }
        public MOM GetMomByReviewIdAndTraineeId(int reviewId,int traineeId,AppDbContext dbContext)
        {
            var reviewExists = Validation.ReviewExists(dbContext,reviewId);
            var traineeExists = Validation.UserExists(dbContext,traineeId);
            if(reviewExists && traineeExists)
            {
                var momExists = Validation.MOMExists(dbContext,reviewId,traineeId);
                if(momExists) 
                {
                    var result = dbContext.MOMs.Where(m=>m.ReviewId==reviewId && m.TraineeId == traineeId).FirstOrDefault();
                    if(result is not null) return result;
                }
            }
            throw new ArgumentException("Inavlid Id's");
        }
        public Dictionary<string,string> CreateMom(MOM mom,AppDbContext dbContext)
        {
            if(mom is null) throw new ArgumentNullException(nameof(mom));
            var validation = Validation.ValidateMOM(mom,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpMomDetails(mom);
                CreateAndSaveMom(mom,dbContext);   
            }
            return validation;
        }
        public Dictionary<string,string> UpdateMom(MOM mom,AppDbContext dbContext)
        {
            if(mom is null) throw new ArgumentNullException(nameof(mom));
            var validation = Validation.ValidateMOM(mom,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            { 
                var dbMom = dbContext.MOMs.Find(mom.ReviewId,mom.TraineeId);
                if(dbMom is not null)
                {
                    SetUpMomDetails(mom, dbMom);
                    UpdateAndSaveMom(dbMom,dbContext);
                }
            }
            validation.Add("Invalid Id","Not Found");   
            return validation;
        }
    }
}