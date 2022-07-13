using TMS.BAL;

namespace TMS.API.Services
{
    public partial class DepartmentService
    {
        /// <summary>
        /// used to SetUpDepartmentDetails .
        /// </summary>
        /// <param name="department"></param>

        private static void SetUpDepartmentDetails(Department department)
        {
            department.isDisabled = false;
            department.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// used to SetUpDepartmentDetails .
        /// </summary>
        /// <param name="department"></param>
        /// <param name="dbDepartment"></param>
        private static void SetUpDepartmentDetails(Department department,Department dbDepartment)
        {
            dbDepartment.Name = department.Name;
            department.UpdatedOn = DateTime.Now;
        }

        /// <summary>
        /// used to SetUpDepartmentDetails .
        /// </summary>
        /// <param name="updatedBy"></param>
        /// <param name="dbDepartment"></param>

        private static void Disable(int updatedBy,Department dbDepartment)
        {
            dbDepartment.isDisabled = true;
            dbDepartment.UpdatedBy = updatedBy;
            dbDepartment.UpdatedOn = DateTime.Now;
        }        
    }
}

