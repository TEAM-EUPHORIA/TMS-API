namespace TMS.API.Services
{
    public interface IUnitOfService
    {
        IAuthService AuthService { get; set; }
        IUserService UserService { get; set; }
        IRoleService RoleService { get; set; }
        IReviewService ReviewService { get; set; }
        IFeedbackService FeedbackService { get; set; }
        IDepartmentService DepartmentService { get; set; }
        ICourseService CourseService { get; set; }
        IValidation Validation { get; set; }

        void Complete();
    }
}