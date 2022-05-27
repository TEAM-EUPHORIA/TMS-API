using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class UserService
    {
         public IEnumerable<User> GetUsersByRole(int roleId,AppDbContext dbContext)
        {
            var roleExists = Validation.RoleExists(dbContext,roleId);           
            if(roleExists) return dbContext.Users.Where(u => u.RoleId == roleId).Include(u=>u.Role).Include(u=>u.Department!).ToList();
            else throw new ArgumentException("Invalid Id");
        }
        public IEnumerable<User> GetUsersByDeptandrole(int departmentId,int roleId,AppDbContext dbContext)
        {
            var departmentExists = Validation.DepartmentExists(dbContext,departmentId);
            var roleExists = Validation.RoleExists(dbContext,roleId); 
            if(departmentExists && roleExists) return dbContext.Users.Where(u => u.DepartmentId == departmentId && u.RoleId==roleId).Include(u=>u.Role).Include(u=>u.Department!).ToList();
            else throw new ArgumentException("Invalid Id's");
        }
        public IEnumerable<User> GetUsersByDepartment(int departmentId,AppDbContext dbContext)
        {
            var departmentExists = Validation.DepartmentExists(dbContext,departmentId);
            if(departmentExists) return dbContext.Users.Where(u => (u.DepartmentId == departmentId)).Include(u=>u.Role).ToList();
            else throw new ArgumentException("Invalid Id");
        }
        public User GetUserById(int id,AppDbContext dbContext)
        {
            var userExists = Validation.UserExists(dbContext,id);
            if(userExists)
            {
                var result = dbContext.Users.Where(u=>u.Id==id).Include(u=>u.Role).Include(u=>u.Department!).FirstOrDefault();
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id"); 
        }
        public Dictionary<string, string> CreateUser(User user,AppDbContext dbContext)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            var validation = Validation.ValidateUser(user, dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpUserDetails(user,dbContext);
                CreateAndSaveUser(user,dbContext);  
            }
            return validation;
        }
        public Dictionary<string,string> UpdateUser(User user,AppDbContext dbContext)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            var validation = Validation.ValidateUser(user, dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbUser = dbContext.Users.Find(user.Id);
                if(dbUser is not null)
                {
                    SetUpUserDetails(user,dbUser);
                    UpdateAndSaveUser(dbUser,dbContext);  
                }
            }
            return validation;
        }
        public bool DisableUser(int userId,AppDbContext dbContext)
        {
            var userExists = Validation.UserExists(dbContext,userId);
            if(userExists)
            {
                var dbUser = dbContext.Users.Find(userId);
                if(dbUser is not null)
                {
                    dbUser.isDisabled = true;
                    dbUser.UpdatedOn = DateTime.UtcNow;
                    UpdateAndSaveUser(dbUser,dbContext);
                }
            }
            return userExists;
        }
    }
}