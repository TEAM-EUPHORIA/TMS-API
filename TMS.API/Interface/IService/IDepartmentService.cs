using TMS.BAL;

namespace TMS.API.Services
{
    public interface IDepartmentService
    {
        Dictionary<string, string> CreateDepartment(Department department);
        bool DisableDepartment(int departmentId, int currentUserId);
        Department GetDepartmentById(int departmentId);
        IEnumerable<Department> GetDepartments();
        Dictionary<string, string> UpdateDepartment(Department department);
    }
}