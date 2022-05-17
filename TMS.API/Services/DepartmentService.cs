using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.BAL;

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


    //     public IEnumerable<Department> GetAllDepartments()
    //     {
             
    //         try
    //         {
    //             var result = _context.Departments.ToList();
    //             if (result != null) return Ok(result);
    //             return NotFound("we are sorry, the thing you requested was not found");
    //         }
    //         catch (System.Exception ex)
    //         {
    //             _logger.LogWarning("There was an error in getting all Departments. please check the db for more information");
    //             _logger.LogError($"error:  " + ex.ToString());
    //             return Problem("we are sorry, some thing went wrong");
    //         }
    //     }
    // }

        public void CreateDepartment(Department department)
        {
            if (department == null) throw new ArgumentException("CreateDepartment requires a vaild Department Object");
            try
            {

                Department dbDepartment = new Department();

                dbDepartment.Name = department.Name;
              
                dbDepartment.isDisabled = false;
                dbDepartment.CreatedOn = DateTime.Now;
                _context.Departments.Add(dbDepartment);
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in Department services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in Department services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

        
        public void UpdateDepartment(Department department)
        {
            if (department == null) throw new ArgumentException("UpdateDepartemt requires a vaild User Object");
            try
            {
                var dbdepartment = _context.Departments.Find(department.Id);
                if (dbdepartment != null)
                {
                    
                    
                    dbdepartment.Name = department.Name;
                   
                    dbdepartment.UpdatedOn = DateTime.Now;
                  
                    _context.Update(dbdepartment);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in department services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in department services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }

        public void DisableDepartment(int departmentId)
        {
            if (departmentId == 0) throw new ArgumentException("Disabledepartment requires a vaild departemnt Object");
            try
            {
                var dbdepartment = _context.Departments.Find(departmentId);

                if (dbdepartment != null)
                {

                    dbdepartment.isDisabled = true;
                    dbdepartment.UpdatedOn = DateTime.Now;

                    _context.Update(dbdepartment);
                    _context.SaveChanges();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogCritical("An Critical error occured in department services. Please check the program.cs, context class and connection string. It happend due to failure of injection of context. ");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical("An Critical error occured in department services. Some external factors are involved. please check the log files to know more about it");
                _logger.LogTrace(ex.ToString());
                throw ex;
            }
        }
    }
}

