using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
        private void SetUpDepartmentDetails(Department department)
        {
            department.isDisabled = false;
            department.CreatedOn = DateTime.Now;
        }
        private void SetUpDepartmentDetails(Department department,Department dbDepartment)
        {
            dbDepartment.Name = department.Name;
            department.UpdatedOn = DateTime.Now;
        }
        private void disable(int currentUserId,Department dbDepartment)
        {
            dbDepartment.isDisabled = true;
            dbDepartment.UpdatedBy = currentUserId;
            dbDepartment.UpdatedOn = DateTime.Now;
        }        
    }
}

