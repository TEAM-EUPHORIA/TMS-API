using TMS.API.Repositories;
using TMS.API.UtilityFunctions;
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
        public UserService(IUnitOfWork repo, ILogger<UserService> logger)
        {
            _repo = repo;
            _stats = _repo.Stats;
            _logger = logger;
        }
        /// <summary>
        /// used to get users by department based on department id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>
        /// IEnumerable user if department is found
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            try
            {
                var departmentExists = _repo.Validation.DepartmentExists(departmentId);
                if (departmentExists)
                {
                    IEnumerable<User> enumerable = _repo.Users.GetUsersByDepartment(departmentId);
                    return enumerable;
                }
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(GetUsersByDepartment));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            try
            {    
                var roleExists = _repo.Validation.RoleExists(roleId);
                if (roleExists) return _repo.Users.GetUsersByRole(roleId);
                else throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(GetUsersByRole));
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public IEnumerable<User> GetUsersByDeptandRole(int departmentId, int roleId)
        {
            try
            {    
                var departmentExists = _repo.Validation.DepartmentExists(departmentId);
                var roleExists = _repo.Validation.RoleExists(roleId);
                if (departmentExists && roleExists) return _repo.Users.GetUsersByDeptandRole(departmentId, roleId);
                else throw new ArgumentException("Invalid Id's");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(GetUsersByDeptandRole));   
                throw;
            }
        }
        /// <summary>
        /// used to get single user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// user if user
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public User GetUserById(int userId)
        {
            try
            {    
                var userExists = _repo.Validation.UserExists(userId);
                if (userExists)
                {
                    var result = _repo.Users.GetUserById(userId);
                    return result;
                }
                throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(GetUserById));   
                throw;
            }
        }
        /// <summary>
        /// used to create a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createdBy"></param>
        /// <returns>
        /// result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> CreateUser(User user, int createdBy)
        {
            try
            {    
                if (user is null) throw new ArgumentNullException(nameof(user));
                var result = _repo.Validation.ValidateUser(user);
                if (result.ContainsKey("IsValid") && !result.ContainsKey("Exists"))
                {
                    SetUpUserDetailsForCreate(user, createdBy);
                    _repo.Users.CreateUser(user);
                    _repo.Complete();
                }
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(CreateUser));   
                throw;
            }
        }
        /// <summary>
        /// used to update a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="updatedBy"></param>
        /// <returns>
        ///  result Dictionary 
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public Dictionary<string, string> UpdateUser(User user, int updatedBy)
        {
            try
            {    
                if (user is null) throw new ArgumentNullException(nameof(user));
                var result = _repo.Validation.ValidateUser(user);
                if (result.ContainsKey("IsValid") && result.ContainsKey("Exists"))
                {
                    var dbUser = _repo.Users.GetUserById(user.Id);
                    SetUpUserDetailsForUpdate(user, dbUser, updatedBy);
                    _repo.Users.UpdateUser(dbUser);
                    _repo.Complete();
                }
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(UpdateUser));   
                throw;
            }
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
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public bool DisableUser(int userId, int updatedBy)
        {
            try
            {    
                var userExists = _repo.Validation.UserExists(userId);
                if (userExists)
                {
                    var dbUser = _repo.Users.GetUserById(userId);
                    Disable(dbUser, updatedBy);
                    _repo.Users.UpdateUser(dbUser);
                    _repo.Complete();
                }
                throw new ArgumentException("Invalid Id");
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.ServiceInjectionFailedAtService(ex,_logger,nameof(UserService),nameof(GetUsersByRole));   
                throw;
            }
        }
    }
}