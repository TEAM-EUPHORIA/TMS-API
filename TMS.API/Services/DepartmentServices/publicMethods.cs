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
            _repo = repo;
            _logger = logger;

        }
        /// <summary>
        /// get the all departments
        /// </summary>
        /// <returns>
        /// enumerable department
        /// </returns>
        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return _repo.Departments.GetDepartments();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(DepartmentService),nameof(GetDepartments));
                throw;
            }
        }
        /// <summary>
        /// used to get department by departmentid
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// department if department is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Department GetDepartmentById(int departmentId)
        {
            try
            {

                var departmentExists = _repo.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    var result = _repo.Departments.GetDepartmentById(departmentId);
                    return result;
                }
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentService), nameof(GetDepartmentById));
                throw;
            }
        }

        /// <summary>
        /// used to create a department.
        /// </summary>
        /// <param name="department"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public Dictionary<string, string> CreateDepartment(Department department, int createdBy)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentService), nameof(CreateDepartment));
                throw;
            }

        }

        /// <summary>
        /// used to update Department
        /// </summary>
        /// <param name="department"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>


        public Dictionary<string, string> UpdateDepartment(Department department, int updatedBy)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentService), nameof(UpdateDepartment));
                throw;
            }
        }

        /// <summary>
        /// used to disable department 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// true if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>

        public bool DisableDepartment(int departmentId, int updatedBy)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex, _logger, nameof(DepartmentService), nameof(DisableDepartment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DisableDepartment));
                throw;
            }
        }
    }
}