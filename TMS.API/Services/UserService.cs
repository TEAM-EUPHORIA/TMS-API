using Microsoft.EntityFrameworkCore;
using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private void GenerateUserId(User user)
        {
            var newId = _context.Users.Count() + 1;
            if (user.DepartmentId != 0 && user.DepartmentId != null) user.EmployeeId = $"ACE{user.RoleId}{user.DepartmentId}{newId}";
            else user.EmployeeId = $"ACE{user.RoleId}0{newId}";
        }
        private static void SetUpImage(User user)
        {
            File Image = FileService.GetBase64HeaderAndByteArray(user.Base64);
            user.Base64 = Image.header;
            user.Image = Image.bytes;
        }
        private void SetUpUserDetails(User user)
        {
            user.isDisabled = false;
            user.Password = HashPassword.Sha256(user.Password);
            if (string.IsNullOrEmpty(user.EmployeeId)) GenerateUserId(user);
            if (!string.IsNullOrEmpty(user.Base64) && user.Base64.Length > 1000) SetUpImage(user);
            user.CreatedOn = DateTime.UtcNow;
        }
        private static void SetUpUserDetails(User user, User dbUser)
        {
            dbUser.isDisabled = false;
            dbUser.Password = HashPassword.Sha256(user.Password);
            dbUser.FullName = user.FullName;
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.Base64) && user.Base64.Length > 1000)
            {
                SetUpImage(user);
                dbUser.Base64 = user.Base64;
                dbUser.Image = user.Image;
            }
            dbUser.UpdatedOn = DateTime.UtcNow;
        }
        private void CreateAndSaveUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        private void UpdateAndSaveUser(User dbUser)
        {
            _context.Users.Update(dbUser);
            _context.SaveChanges();
        }
        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            if (roleId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUsersByRole));
            try
            {
                return _context.Users.Where(u => u.RoleId == roleId).Include("Role").Include("Department").ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUsersByRole));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByRole));
                throw;
            }
        }

        public IEnumerable<User> GetUsersByDeptandrole(int did,int rid)
        {
            if (did == 0 || rid==0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUsersByDeptandrole));
            try
            {
                return _context.Users.Where(u => u.DepartmentId == did && u.RoleId==rid).Include("Role").Include("Department").ToList();
                
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUsersByDeptandrole));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByDeptandrole));
                throw;
            }
        }

        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            if (departmentId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUsersByDepartment));
            try
            {
                return _context.Users.Where(u => (u.DepartmentId != 0 && u.DepartmentId == departmentId)).Include("Role").ToList();
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUsersByDepartment));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUsersByDepartment));
                throw;
            }
        }
        public User GetUserById(int id)
        {
            if (id == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(GetUserById));
            try
            {
                var dbUser = _context.Users.Find(id);
                User result = new User();
                if (dbUser != null)
                {
                    string base64String = Convert.ToBase64String(dbUser.Image, 0, dbUser.Image.Length);
                    result.Base64 = result.Base64 + base64String;
                    if (dbUser.DepartmentId != null)
                    {
                        result = _context.Users.Where(u => u.Id == id).Include("Role").Include("Department").FirstOrDefault();
                    }
                    else
                    {
                        result = _context.Users.Where(u => u.Id == id).Include("Role").FirstOrDefault();
                    }
                }
                return result;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(GetUserById));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(GetUserById));
                throw;
            }
        }
        public bool CreateUser(User user)
        {
            if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(CreateUser), nameof(user));
            try
            {
                SetUpUserDetails(user);
                CreateAndSaveUser(user);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(CreateUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(CreateUser));
                throw;
            }

        }

        public bool UpdateUser(User user)
        {
            if (user == null) ServiceExceptions.throwArgumentExceptionForObject(nameof(UpdateUser), nameof(user));
            try
            {
                var dbUser = _context.Users.Find(user.Id);
                if (dbUser != null)
                {
                    SetUpUserDetails(user, dbUser);
                    UpdateAndSaveUser(dbUser);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(UpdateUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(UpdateUser));
                throw;
            }
        }

        public bool DisableUser(int userId)
        {
            if (userId == 0) ServiceExceptions.throwArgumentExceptionForId(nameof(DisableUser));
            try
            {
                var dbUser = _context.Users.Find(userId);
                if (dbUser != null)
                {
                    dbUser.isDisabled = true;
                    dbUser.UpdatedOn = DateTime.Now;
                    UpdateAndSaveUser(dbUser);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                TMSLogger.DbContextInjectionFailed(ex, _logger, nameof(UserService), nameof(DisableUser));
                throw;
            }
            catch (Exception ex)
            {
                TMSLogger.GeneralException(ex, _logger, nameof(UserService), nameof(DisableUser));
                throw;
            }
        }
    }
}

