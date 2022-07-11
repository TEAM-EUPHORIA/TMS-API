using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;

        public DepartmentService(IUnitOfWork repo,ILogger logger)
        {
            _repo = repo;
            _logger = logger;

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

        public Dictionary<string, string> CreateDepartment(Department department, int createdBy)
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

        public Dictionary<string, string> UpdateDepartment(Department department, int updatedBy)
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

        public bool DisableDepartment(int departmentId, int updatedby)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                var dbDeparment = _repo.Departments.GetDepartmentById(departmentId);
                Disable(updatedby, dbDeparment);
                _repo.Departments.UpdateDepartment(dbDeparment);
                _repo.Complete();
            }
            return departmentExists;
        }
    }
}