using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
        private static void SetUpDepartmentDetails(Department department)
        {
            department.isDisabled = false;
            department.CreatedOn = DateTime.Now;
        }
        private static void SetUpDepartmentDetails(Department department,Department dbDepartment)
        {
            dbDepartment.Name = department.Name;
            department.UpdatedOn = DateTime.Now;
        }
        private static void Disable(int updatedBy,Department dbDepartment)
        {
            dbDepartment.isDisabled = true;
            dbDepartment.UpdatedBy = updatedBy;
            dbDepartment.UpdatedOn = DateTime.Now;
        }        
    }
}

