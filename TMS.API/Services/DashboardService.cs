namespace TMS.API.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DashboardService> _logger;
        private readonly UserService _userService;
        private readonly CourseService _courseService;
        private readonly DepartmentService _departmentService;

        public DashboardService(AppDbContext context, ILogger<DashboardService> logger, UserService userService, CourseService courseService, DepartmentService departmentService, ReviewService reviewService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _courseService = courseService;
            _departmentService = departmentService;
        }
        public object getUserCount()
        {
            int coordinatorCount = _context.Users.Where(u => u.RoleId == 1).Count();
            int trainerCount = _context.Users.Where(u => u.RoleId == 2).Count();
            int traineeCount = _context.Users.Where(u => u.RoleId == 3).Count();
            int reviewerCount = _context.Users.Where(u => u.RoleId == 4).Count();
            //int reviewerCount = _userService.GetUsersByRole(2).Count();
            return new
            {
                coordinatorCount,
                trainerCount,
                traineeCount,
                reviewerCount
            };
        }

        public object getCourseCount()
        {
            int courseCount = _context.Courses.Count();
            return new
            {
                courseCount
            };
        }

        public object getReviewCountByStatus()
        {
            int completedReviewCount = _context.Reviews.Where(u => u.StatusId == 1).Count();
            int cancelledReviewCount = _context.Reviews.Where(u => u.StatusId == 2).Count();
            return new{
                completedReviewCount,
                cancelledReviewCount
            };
        }
        public object getDepartmentCount(){
            int departmentCount = _context.Departments.Count();
            return new{
                departmentCount
                };
        }

    }
}