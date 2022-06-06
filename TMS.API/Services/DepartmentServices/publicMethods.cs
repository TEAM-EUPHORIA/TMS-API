using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
        public IEnumerable<Department> GetDepartments(AppDbContext dbContext)
        {
            return dbContext.Departments.ToList();            
        }

        public Department GetDepartmentById(int id,AppDbContext dbContext)
        {
            var departmentExists = Validation.DepartmentExists(dbContext,id);
            if(departmentExists)
            {
                var result = dbContext.Departments.Find(id);
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id");
        }
        
        public Dictionary<string, string> CreateDepartment(Department department,AppDbContext dbContext)
        {
            if(department is null) throw new ArgumentNullException(nameof(department));
            var validation = Validation.ValidateDepartment(department,dbContext);
            if (validation.ContainsKey("IsValid" )&& !validation.ContainsKey("Exists"))
            {
                SetUpDepartmentDetails(department);
                CreateAndSaveDepartment(department,dbContext);  
            }
            return validation;
        }

        public Dictionary<string,string> UpdateDepartment(Department department,AppDbContext dbContext)
        {
            if (department is null) throw new ArgumentNullException(nameof(department));
            var validation = Validation.ValidateDepartment(department,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                var dbDeparment = dbContext.Departments.Find(department.Id);
                if(dbDeparment is not null)
                {
                    SetUpDepartmentDetails(department, dbDeparment);
                    UpdateAndSaveDepartment(dbDeparment,dbContext);     
                }
            }
            return validation;
        }

        public bool DisableDepartment(int departmentId,AppDbContext dbContext)
        {
            var departmentExists = Validation.DepartmentExists(dbContext,departmentId);
            if(departmentExists)
            {
                var dbDepartment = dbContext.Departments.Find(departmentId);
                if(dbDepartment is not null)
                {
                    dbDepartment.isDisabled = true;
                    dbDepartment.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveDepartment(dbDepartment,dbContext);
                }
            }
            return departmentExists;
        }
    }
}