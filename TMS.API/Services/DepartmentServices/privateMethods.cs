using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
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

