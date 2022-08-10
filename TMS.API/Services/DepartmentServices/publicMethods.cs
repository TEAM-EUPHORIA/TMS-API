using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _repo;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor of DepartmentService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public DepartmentService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
        /// <summary>
        /// get the all departments
        /// </summary>
        /// <returns>
        /// enumerable department
        /// </returns>
        public IEnumerable<Department> GetDepartments()
        {
            return _repo.Departments.GetDepartments();
        }
        /// <summary>
        /// used to get department by departmentid
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// department if department is found
        /// </returns>
        public Department GetDepartmentById(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                var result = _repo.Departments.GetDepartmentById(departmentId);
                return result;
            }
            else throw new ArgumentException("Invalid Id");
        }
        /// <summary>
        /// used to create a department.
        /// </summary>
        /// <param name="department"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        public Dictionary<string, string> CreateDepartment(Department department, int createdBy)
        {
            if (department is null) throw new ArgumentException(nameof(department));
            var validation = _repo.Validation.ValidateDepartment(department);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpDepartmentDetails(department);
                _repo.Departments.CreateDepartment(department);
                _repo.Complete();
            }
            return validation;
        }
        /// <summary>
        /// used to update Department
        /// </summary>
        /// <param name="department"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        public Dictionary<string, string> UpdateDepartment(Department department, int updatedBy)
        {
            if (department is null) throw new ArgumentException(nameof(department));
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
        /// <summary>
        /// used to disable department 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// true if user is found
        /// </returns>
        public bool DisableDepartment(int departmentId, int updatedBy)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                var dbDeparment = _repo.Departments.GetDepartmentById(departmentId);
                Disable(updatedBy, dbDeparment);
                _repo.Departments.UpdateDepartment(dbDeparment);
                _repo.Complete();
            }
            return departmentExists;
            throw new ArgumentException("Invalid Id");
        }
    }
}