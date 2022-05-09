using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<User> GetUsersByRole(int roleId)
        {
            if (roleId == 0) throw new ArgumentException("GetUsersByRole requires a vaild Id not zero");
            try
            {
                return _context.Users.Where(u => u.RoleId == roleId).Include("Role").Include("Department").ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

        public IEnumerable<User> GetUsersByDepartment(int departmentId)
        {
            if (departmentId == 0) throw new Exception("GetUsersByDepartment requires a vaild Id not zero");
            try
            {
                return _context.Users.Where(u => (u.DepartmentId != 0 && u.DepartmentId == departmentId)).ToList();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

        public Object GetUsersById(int id)
        {
            var dbUser = _context.Users.Where(u => u.Id == id).Include("Role").Include("Department").FirstOrDefault();
            if (dbUser != null)
            {
                var _DepartmentId = (dbUser.DepartmentId != null) ? dbUser.DepartmentId : 0;
                var _Department = (dbUser.DepartmentId != null) ? dbUser.Department.Name : "";
                var result = new
                {
                    Id = dbUser.Id,
                    RoleId = dbUser.RoleId,
                    Role = dbUser.Role.Name,
                    DepartmentId = _DepartmentId,
                    Department = _Department,
                    Name = dbUser.Name,
                    UserName = dbUser.UserName,
                    Image = dbUser.Image
                };

                return result;
            }
            return "not found";
        }

        public void CreateUser(UserDTO user)
        {
            if (user == null) throw new ArgumentException("CreateUser requires a vaild User Object");
            try
            {
                Random ran = new Random();
                User dbUser = new User();
                dbUser.RoleId = user.RoleId;
                dbUser.DepartmentId = user.DepartmentId;
                dbUser.Name = user.Name;
                dbUser.UserName = user.UserName;
                dbUser.Password = user.Password;
                dbUser.Email = user.Email;
                if (user.Image != null)
                {
                    dbUser.Image = user.Image;
                }
                dbUser.EmployeeId = ($"ACE{user.RoleId}{ran.Next(0, 10000)}");
                dbUser.isDisabled = false;
                dbUser.CreatedOn = DateTime.Now;
                _context.Users.Add(dbUser);
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void UpdateUser(UserDTO user)
        {
            if (user == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
            try
            {
                var dbUser = _context.Users.Find(user.Id);
                if (dbUser != null)
                {
                    dbUser.RoleId = user.RoleId;
                    dbUser.DepartmentId = user.DepartmentId;
                    dbUser.Name = user.Name;
                    dbUser.UserName = user.UserName;
                    dbUser.Password = user.Password;
                    dbUser.Email = user.Email;
                    dbUser.UpdatedOn = DateTime.Now;
                    if (user.Image != null)
                    {
                        dbUser.Image = user.Image;
                    }
                    _context.Update(dbUser);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void DisableUser(int userId)
        {
            if (userId == 0) throw new ArgumentException("DisableUser requires a vaild User Object");
            try
            {
                var dbUser = _context.Users.Find(userId);
                if (dbUser != null)
                {

                    dbUser.isDisabled = true;
                    dbUser.UpdatedOn = DateTime.Now;

                    _context.Update(dbUser);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
        public void DeleteUser(int id)
        {
            if (id == 0) throw new ArgumentException("DeleteUser requires a vaild User Object");
            try
            {
                var dbUser = _context.Users.Find(id);
                if (dbUser != null)
                {
                    if (dbUser.isDisabled == true)
                    {
                        _context.Remove(dbUser);
                        _context.SaveChanges();
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

    }
}