using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class UserService
    {
        private readonly IUnitOfWork _repo;
        private readonly IStatistics _stats;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor of UserService
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public UserService(IUnitOfWork repo, ILogger logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _stats = _repo.Stats;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// used to get users by department based on department id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// IEnumerable user if department is found
        /// </returns>
        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            if (departmentExists)
            {
                IEnumerable<User> enumerable = _repo.Users.GetUsersByDepartment(departmentId);
                return enumerable;
            }
            else throw new ArgumentException("Invalid Id");
        }
        /// <summary>
        /// used to get users by role based on role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>
        /// IEnumerable user if role is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            var roleExists = _repo.Validation.RoleExists(roleId);
            if (roleExists) return _repo.Users.GetUsersByRole(roleId);
            else throw new ArgumentException("Invalid Id");
        }
        /// <summary>
        /// used to get users by department and role based on department id and role id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns>
        /// IEnumerable user if department and role is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IEnumerable<User> GetUsersByDeptandRole(int departmentId, int roleId)
        {
            var departmentExists = _repo.Validation.DepartmentExists(departmentId);
            var roleExists = _repo.Validation.RoleExists(roleId);
            if (departmentExists && roleExists) return _repo.Users.GetUsersByDeptandRole(departmentId, roleId);
            else throw new ArgumentException("Invalid Id's");
        }
        /// <summary>
        /// used to get single user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// user if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public User GetUser(int userId)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists)
            {
                return _repo.Users.GetUserById(userId);
            }
            else throw new ArgumentException("Invalid Id");
        }
        /// <summary>
        /// used to create a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> CreateUser(User user, int createdBy)
        {
            if (user is null) throw new ArgumentException(nameof(user));
            var result = _repo.Validation.ValidateUser(user);
            if (result.ContainsKey("IsValid") && !result.ContainsKey("Exists"))
            {
                SetUpUserDetailsForCreate(user, createdBy);
                _repo.Users.CreateUser(user);
                _repo.Complete();
            }
            return result;
        }
        /// <summary>
        /// used to update a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public Dictionary<string, string> UpdateUser(UpdateUserModel user, int updatedBy)
        {
            if (user is null) throw new ArgumentException(nameof(user));
            var result = _repo.Validation.ValidateUpdtateUser(user);
            if (result.ContainsKey("IsValid") && result.ContainsKey("Exists"))
            {
                var dbUser = _repo.Users.GetUserById(user.Id);
                SetUpUserDetailsForUpdate(user, dbUser, updatedBy);
                _repo.Users.UpdateUser(dbUser);
                _repo.Complete();
            }
            return result;
        }
        /// <summary>
        /// used to disable the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        /// true if user is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public bool DisableUser(int userId, int updatedBy)
        {
            var userExists = _repo.Validation.UserExists(userId);
            if (userExists)
            {
                var dbUser = _repo.Users.GetUserById(userId);
                Disable(dbUser, updatedBy);
                _repo.Users.UpdateUser(dbUser);
                _repo.Complete();
            }
            return userExists;
            throw new ArgumentException("Invalid Id");
        }
    }
}