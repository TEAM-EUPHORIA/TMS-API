namespace TMS.API.Services
{
    public partial class UserService
    {
        public object GetStats()
        {
            int coordinatorCount = _context.Users.Where(u => u.RoleId == 2).Count();
            int trainerCount = _context.Users.Where(u => u.RoleId == 3).Count();
            int traineeCount = _context.Users.Where(u => u.RoleId == 4).Count();
            int reviewerCount = _context.Users.Where(u => u.RoleId == 5).Count();
            int courseCount =  _context.Courses.Count();
            int departmentCount =  _context.Departments.Count();
            int completedReviewCount = _context.Reviews.Where(u => u.StatusId == 2).Count();
            int cancelledReviewCount = _context.Reviews.Where(u => u.StatusId == 3).Count();
            return new
            {
                coordinatorCount,
                trainerCount,
                traineeCount,
                reviewerCount,
                courseCount,
                departmentCount,
                completedReviewCount,
                cancelledReviewCount
            };
        }
    }
}