using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.API.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateUser(User user)
        {
            dbContext.Users.Add(user);
        }

        public void DisableUser(int userId)
        {
            var data = dbContext.Users.Find(userId);
            if(data!=null)
            {
                data.isDisabled = true;
                dbContext.Users.Update(data);
            }
        }
        public User GetUserByEmailAndPassword(LoginModel user)
        {
            return dbContext.Users.Where(u => u.Email == user.Email && u.Password == HashPassword.Sha256(user.Password)).Include(u => u.Role).FirstOrDefault();
        }

        public User GetUserById(int id)
        {
            return dbContext.Users.Where(u=>u.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsersByDeptandrole(int departmentId, int roleId)
        {
            return dbContext.Users.Where(u=>u.DepartmentId == departmentId && u.RoleId == roleId);
        }

        public void UpdateUser(User user)
        {
            dbContext.Users.Update(user);
        }
    }
}