using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class UserRepository : IUserRepository
    {
        public void CreateUser(User user)
        {
            dbContext.Users.Add(user);
        }
        public User GetUserByEmailAndPassword(LoginModel user)
        {
            return dbContext.Users
                    .Where(u => u.Email == user.Email &&
                           u.Password == HashPassword.Sha256(user.Password))
                    .Include(u => u.Role).FirstOrDefault()!;
        }
        public User GetUserById(int id)
        {
            return dbContext.Users
                    .Where(u => u.Id == id)
                    .Include(u => u.Role)
                    .Include(u => u.Department)
                    .FirstOrDefault()!;
        }
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            return dbContext.Users
                    .Where(u => u.RoleId == roleId && u.isDisabled != true)
                    .Include(u => u.Department)
                    .Include(u => u.Role);
        }
        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            return dbContext.Users
                    .Where(u => u.DepartmentId == departmentId && u.isDisabled == false)
                    .Include(u => u.Department)
                    .Include(u => u.Role);
        }
        public IEnumerable<User> GetUsersByDeptandRole(int departmentId, int roleId)
        {
            return dbContext.Users
                    .Where(u => u.DepartmentId == departmentId && u.RoleId == roleId && u.isDisabled == false)
                    .Include(u => u.Department)
                    .Include(u => u.Role);
        }
        public void UpdateUser(User user)
        {
            dbContext.Users.Update(user);
        }
    }
}