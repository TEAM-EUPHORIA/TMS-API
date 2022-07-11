using TMS.BAL;

namespace TMS.API.Services
{
    public interface IDepartmentService
    {
        Dictionary<string, string> CreateDepartment(Department department, int createdBy);
        bool DisableDepartment(int departmentId, int updatedBy);
        Department GetDepartmentById(int departmentId);
        IEnumerable<Department> GetDepartments();
        Dictionary<string, string> UpdateDepartment(Department department, int updatedBy);
    }
}