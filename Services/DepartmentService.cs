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
    }
}