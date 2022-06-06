namespace TMS.API.Services
{
    public partial class UserService
    {
        private int CoordinatorCount(AppDbContext dbContext)
        {
            return dbContext.Users.Where(u => u.RoleId == 2).Count();
        }
        private int DepartmentCount(AppDbContext dbContext)
        {
            return dbContext.Departments.Count();
        }
        private int TraineeCount(AppDbContext dbContext)
        {
            return dbContext.Users.Where(u => u.RoleId == 4).Count();
        }
        private int TrainerCount(AppDbContext dbContext)
        {
            return dbContext.Users.Where(u => u.RoleId == 3).Count();
        }
        
        private int ReviewerCount(AppDbContext dbContext)
        {
            return dbContext.Users.Where(u => u.RoleId == 5).Count();
        }
        private int CourseCount(AppDbContext dbContext)
        {
            return dbContext.Courses.Count();
        }
        private int CourseCompleted(AppDbContext dbContext,int userId)
        {
             throw new NotImplementedException();
        }
        private int CourseInprogress(AppDbContext dbContext,int userId)
        {
             throw new NotImplementedException();
        }
        private int CompletedReviewCount(AppDbContext dbContext,int userId)
        {
             throw new NotImplementedException();
        }
        private int UpcomigReviewCount(AppDbContext dbContext,int userId)
        {
             throw new NotImplementedException();
        }
        private void prepareHeadDashboard(Dictionary<string,int> result,AppDbContext dbContext)
        {
            var coordinatorCount = CoordinatorCount(dbContext);
            int departmentCount, trainerCount, traineeCount;
            result.Add(nameof(coordinatorCount), coordinatorCount);
            AddDataToResult(result, dbContext, out departmentCount, out trainerCount, out traineeCount);
        }

        private void AddDataToResult(Dictionary<string, int> result, AppDbContext dbContext, out int departmentCount, out int trainerCount, out int traineeCount)
        {
            GetDepartmentCountTrainerCountAndTraineeCount(dbContext, out departmentCount, out trainerCount, out traineeCount);
            AddDepartmentCountTrainerCountAndTraineeCount(result, departmentCount, trainerCount, traineeCount);
        }

        private static void AddDepartmentCountTrainerCountAndTraineeCount(Dictionary<string, int> result, int departmentCount, int trainerCount, int traineeCount)
        {
            result.Add(nameof(departmentCount), departmentCount);
            result.Add(nameof(trainerCount), trainerCount);
            result.Add(nameof(traineeCount), traineeCount);
        }

        private void GetDepartmentCountTrainerCountAndTraineeCount(AppDbContext dbContext, out int departmentCount, out int trainerCount, out int traineeCount)
        {
            departmentCount = DepartmentCount(dbContext);
            trainerCount = TrainerCount(dbContext);
            traineeCount = TraineeCount(dbContext);
        }

        private void prepareCoordinatorDashboard(Dictionary<string, int> result, AppDbContext dbContext)
        {
            var courseCount = CourseCount(dbContext);
            int departmentCount, trainerCount, traineeCount;
            result.Add(nameof(courseCount), courseCount);
            AddDataToResult(result, dbContext, out departmentCount, out trainerCount, out traineeCount);
        }
        private void prepareTraineeDashboard(Dictionary<string, int> result, AppDbContext dbContext)
        {
            throw new NotImplementedException();
        }
        private void prepareTrainerDashboard(Dictionary<string, int> result, AppDbContext dbContext)
        {
            throw new NotImplementedException();
        }
        private void prepareReviewerDashboard(Dictionary<string, int> result, AppDbContext dbContext)
        {
            throw new NotImplementedException();
        }        
    }
}