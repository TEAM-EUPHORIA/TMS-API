using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class DepartmentService
    {

        private readonly AppDbContext _context;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(AppDbContext context, ILogger<DepartmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

         public Object GetDepartmentByUserId(int id)
        {
            var dbdepartment = _context.Departments.Where(u => u.Id == id).FirstOrDefault();
            if (dbdepartment != null)
            {

                var result = new
                {
                    Id = dbdepartment.Id,
    
                    Name = dbdepartment.Name
                    
                };

                return result;
            }
            return "not found";
        }

        public void CreateDepartment(DepartmentDTO department)
        {
            if (department == null) throw new ArgumentException("CreateDepartment requires a vaild Department Object");
            try
            {
                Random ran = new Random();
                Department dbDepartment = new Department();
        
                dbDepartment.Id = department.Id;
                dbDepartment.Name = department.Name;
              
                dbDepartment.isDisabled = false;
                dbDepartment.CreatedOn = DateTime.Now;
                _context.Departments.Add(dbDepartment);
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

        // public void CreateUser(UserDTO user)
        // {
        //     if (user == null) throw new ArgumentException("CreateUser requires a vaild User Object");
        //     try
        //     {
        //         Random ran = new Random();
        //         User dbUser = new User();
        //         dbUser.RoleId = user.RoleId;
        //         dbUser.DepartmentId = user.DepartmentId;
        //         dbUser.Name = user.Name;
        //         dbUser.UserName = user.UserName;
        //         dbUser.Password = user.Password;
        //         dbUser.Email = user.Email;
        //         if (user.Image != null)
        //         {
        //             dbUser.Image = user.Image;
        //         }
        //         dbUser.EmployeeId = ($"ACE{user.RoleId}{ran.Next(0, 10000)}");
        //         dbUser.isDisabled = false;
        //         dbUser.CreatedOn = DateTime.Now;
        //         _context.Users.Add(dbUser);
        //         _context.SaveChanges();
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }
        // public void UpdateUser(UserDTO user)
        // {
        //     if (user == null) throw new ArgumentException("UpdateUser requires a vaild User Object");
        //     try
        //     {
        //         var dbUser = _context.Users.Find(user.Id);
        //         if (dbUser != null)
        //         {
        //             dbUser.RoleId = user.RoleId;
        //             dbUser.DepartmentId = user.DepartmentId;
        //             dbUser.Name = user.Name;
        //             dbUser.UserName = user.UserName;
        //             dbUser.Password = user.Password;
        //             dbUser.Email = user.Email;
        //             dbUser.UpdatedOn = DateTime.Now;
        //             if (user.Image != null)
        //             {
        //                 dbUser.Image = user.Image;
        //             }
        //             _context.Update(dbUser);
        //             _context.SaveChanges();
        //         }
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }

        // public void DisableUser(int userId)
        // {
        //     if (userId == 0) throw new ArgumentException("DisableUser requires a vaild User Object");
        //     try
        //     {
        //         var dbUser = _context.Users.Find(userId);

        //         if (dbUser != null)
        //         {

        //             dbUser.isDisabled = true;
        //             dbUser.UpdatedOn = DateTime.Now;

        //             _context.Update(dbUser);
        //             _context.SaveChanges();
        //         }
        //     }
        //     catch (System.InvalidOperationException ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         _logger.LogCritical("An Critical error occured in User services. Some external factors are involved. please check the log files to know more about it");
        //         _logger.LogTrace(ex.ToString());
        //         throw ex;
        //     }
        // }
    }
}