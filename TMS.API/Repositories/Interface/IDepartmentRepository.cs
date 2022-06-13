using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<User> GetUsersByDepartment(int departmentId);
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentById(int id);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DisableDepartment(int departmentId);
    }
}