using TMS.BAL;

namespace TMS.API.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentById(int departmentId);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
    }
}