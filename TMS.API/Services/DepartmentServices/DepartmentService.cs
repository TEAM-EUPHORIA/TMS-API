using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class DepartmentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(AppDbContext context, ILogger<DepartmentService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(DepartmentService), nameof(GetDepartments));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(GetDepartments));
                throw;
            }
        }

        public Department GetDepartmentById(int id)
        {
            if (id == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetDepartmentById));
            try
            {
                var result = _context.Departments.Find(id);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(DepartmentService), nameof(GetDepartmentById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(GetDepartmentById));
                throw;
            }
        }
        
        public Dictionary<string, string> CreateDepartment(Department department)
        {
            if (department == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateDepartment), nameof(department));
            var validation = Validation.ValidateDepartment(department);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    SetUpDepartmentDetails(department);
                    CreateAndSaveDepartment(department);
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(DepartmentService), nameof(CreateDepartment));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(CreateDepartment));
                    throw;
                }
            }
            return validation;
        }

        public Dictionary<string,string> UpdateDepartment(Department department)
        {
            if (department == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateDepartment), nameof(department));
            var validation = Validation.ValidateDepartment(department);
            if (validation.ContainsKey("IsValid"))
            {
                try
                {
                    var dbDeparment = _context.Departments.Find(department.Id);
                    if (dbDeparment != null)
                    {
                        SetUpDepartmentDetails(department, dbDeparment);
                        UpdateAndSaveDepartment(dbDeparment);
                    }
                    validation.Add("Invalid Id","Not Found");
                }
                catch (InvalidOperationException ex)
                {
                    TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(DepartmentService), nameof(UpdateDepartment));
                    throw;
                }
                catch (Exception ex)
                {
                    TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(UpdateDepartment));
                    throw;
                }
            }
            return validation;
        }

        public bool DisableDepartment(int departmentId)
        {
            if (departmentId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableDepartment));
            try
            {
                var dbDepartment = _context.Departments.Find(departmentId);
                if (dbDepartment != null)
                {
                    dbDepartment.isDisabled = true;
                    dbDepartment.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveDepartment(dbDepartment);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(DepartmentService), nameof(DisableDepartment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(DepartmentService), nameof(DisableDepartment));
                throw;
            }
        }
        private void UpdateAndSaveDepartment(Department deparment)
        {
            _context.Departments.Update(deparment);
            _context.SaveChanges();
        }
        private void CreateAndSaveDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }
        private void SetUpDepartmentDetails(Department department)
        {
            department.isDisabled = false;
            department.CreatedOn = DateTime.UtcNow;
        }
        private void SetUpDepartmentDetails(Department department,Department dbDepartment)
        {
            dbDepartment.Name = department.Name;
            department.UpdatedOn = DateTime.UtcNow;
        }        
    }
}

