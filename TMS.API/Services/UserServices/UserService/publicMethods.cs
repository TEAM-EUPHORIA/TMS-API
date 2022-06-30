using Microsoft.EntityFrameworkCore;
using TMS.API.Repositories;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class UserService
    {
        private readonly IUnitOfWork _repo;
        private readonly Statistics _stats;

        public UserService(IUnitOfWork repo,Statistics stats)
        {
            _repo = repo;
            _stats = _repo.Stats;
        }
        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists) return _repo.Users.GetUsersByDepartment(departmentId);
            else throw new ArgumentException("Invalid Id");
        }
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            var roleExists = _repo.Validation.RoleExists(roleId);
            if (roleExists) return _repo.Users.GetUsersByRole(roleId);
            else throw new ArgumentException("Invalid Id");
        }
        public IEnumerable<User> GetUsersByDeptandrole(int departmentId, int roleId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            var roleExists = _repo.Validation.RoleExists(roleId);
            if (departmentExists && roleExists) return _repo.Users.GetUsersByDeptandrole(departmentId,roleId);
            else throw new ArgumentException("Invalid Id's");
        }
        public User GetUserById(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists)
            {
                var result =  _repo.Users.GetUserById(userId);
                return result;
            }
            throw new ArgumentException("Invalid Id");
        }
        public Dictionary<string, string> CreateUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            var validation = _repo.Validation.ValidateUser(user);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                SetUpUserDetails(user);
                _repo.Users.CreateUser(user);
                _repo.Complete();
            }
            return validation;
        }
        public Dictionary<string, string> UpdateUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            var validation = _repo.Validation.ValidateUser(user);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {
                var dbUser = _repo.Users.GetUserById(user.Id);
                SetUpUserDetails(user, dbUser);
                _repo.Users.UpdateUser(dbUser);  
                _repo.Complete();
            }
            return validation;
        }
        public bool DisableUser(int userId, int currentUserId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists)
            {
                var dbUser = _repo.Users.GetUserById(userId);
                disable(currentUserId, dbUser);
                _repo.Users.UpdateUser(dbUser);
                _repo.Complete();
            }
            return userExists;
        }
    }
}