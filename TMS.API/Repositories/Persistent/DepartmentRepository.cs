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

        public Department GetDepartmentById(int departmentId)
        {
            return dbContext.Departments.Find(departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return dbContext.Departments.Where(u=>u.isDisabled != true).ToList();
        }

        public void UpdateDepartment(Department department)
        {
            dbContext.Departments.Update(department);
        }
    }
}