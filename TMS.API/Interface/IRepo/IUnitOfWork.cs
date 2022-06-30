using TMS.API.Services;

namespace TMS.API.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; set; }
        IRoleRepository Roles { get; set; }
        ICourseRepository Courses { get; set; }
        IReviewRepository Reviews { get; set; }
        IFeedbackRepository Feedbacks { get; set; }
        IDepartmentRepository Departments { get; set; }
        IValidation Validation { get; set; }
        Statistics Stats { get; set; }

        void Complete();
    }
}