using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
        private void UpdateAndSaveDepartment(Department deparment,AppDbContext dbContext)
        {
            dbContext.Departments.Update(deparment);
            dbContext.SaveChanges();
        }
        private void CreateAndSaveDepartment(Department department,AppDbContext dbContext)
        {
            dbContext.Departments.Add(department);
            dbContext.SaveChanges();
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

