using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<User> GetUsersByDepartment(int departmentId, AppDbContext dbContext);
        IEnumerable<Department> GetDepartments(AppDbContext dbContext);
        Department GetDepartmentById(int id, AppDbContext dbContext);
        void CreateDepartment(Department department, AppDbContext dbContext);
        void UpdateDepartment(Department department, AppDbContext dbContext);
        void DisableDepartment(int departmentId, AppDbContext dbContext);
    }
}