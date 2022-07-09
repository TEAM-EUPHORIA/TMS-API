using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class ReviewRepository : IReviewRepository
    {
        public void CreateMom(MOM mom)
        {
            var review = dbContext.Reviews.Find(mom.ReviewId);
            review!.StatusId = 2;
            dbContext.MOMs.Add(mom);
        }
        public void UpdateMom(MOM mom)
        {
            dbContext.MOMs.Update(mom);
        }
        public IEnumerable<MOM> GetListOfMomByUserId(int userId)
        {
            return dbContext.MOMs
                    .Where(m => m.TraineeId == userId)
                    .Include(m => m.Trainee);
        }
        public MOM GetMomByReviewIdAndTraineeId(int reviewId, int traineeId)
        {
            return dbContext.MOMs
                    .Where(m => m.ReviewId == reviewId &&
                                m.TraineeId == traineeId)
                    .Include(m => m.Review)
                    .ThenInclude(r => r!.Reviewer)
                    .Include(m => m.Trainee)
                    .FirstOrDefault()!;
        }
    }
}