using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
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

    public partial class DepartmentService : IDepartmentService
    {
        private readonly UnitOfWork _repo;


        public DepartmentService(UnitOfWork repo)
        {
            _repo = repo;

        }
        public IEnumerable<Department> GetDepartments()
        {
            return _repo.Departments.GetDepartments();
        }
        public Department GetDepartmentById(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                var result = _repo.Departments.GetDepartmentById(departmentId);
                return result;
            }
            throw new ArgumentException("Invalid Id");
        }

        public Dictionary<string, string> CreateDepartment(Department department)
        {
            if (department is null) throw new ArgumentNullException(nameof(department));
            var validation = _repo.Validation.ValidateDepartment(department);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpDepartmentDetails(department);
                _repo.Departments.CreateDepartment(department);
                _repo.Complete();
            }
            return validation;
        }

        public Dictionary<string, string> UpdateDepartment(Department department)
        {
            if (department is null) throw new ArgumentNullException(nameof(department));
            var validation = _repo.Validation.ValidateDepartment(department);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbDeparment = _repo.Departments.GetDepartmentById(department.Id);
                SetUpDepartmentDetails(department, dbDeparment);
                _repo.Departments.UpdateDepartment(dbDeparment);
                _repo.Complete();
            }
            return validation;
        }

        public bool DisableDepartment(int departmentId, int currentUserId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                var dbDeparment = _repo.Departments.GetDepartmentById(departmentId);
                disable(currentUserId, dbDeparment);
                _repo.Departments.UpdateDepartment(dbDeparment);
                _repo.Complete();
            }
            return departmentExists;
        }
    }
}