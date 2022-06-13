using TMS.BAL;

namespace TMS.API.Repositories
{
    class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateDepartment(Department department)
        {
            dbContext.Departments.Add(department);
        }

        public void DisableDepartment(int departmentId)
        {
            var data = dbContext.Departments.Find(departmentId);
            if(data!=null)
            {
                data.isDisabled = true;
                dbContext.Departments.Update(data);
            }
        }

        public Department GetDepartmentById(int departmentId)
        {
            return dbContext.Departments.Find(departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return dbContext.Departments.ToList();
        }

        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            return dbContext.Users.Where(u=>u.DepartmentId == departmentId);
        }

        public void UpdateDepartment(Department department)
        {
            dbContext.Departments.Update(department);
        }
    }
}