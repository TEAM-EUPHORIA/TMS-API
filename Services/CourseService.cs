using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.API.DTO;
using TMS.API.Models;

namespace TMS.API.Services
{
    public class CourseService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CourseService> _logger;

        public CourseService(AppDbContext context, ILogger<CourseService> logger)
        {
            _context = context;
            _logger = logger;
        }

         public Object GetTopicById(int id)
        {
            var dbTopic = _context.Topics.Where(u => u.Id == id).Include("Course").FirstOrDefault();
            if (dbTopic != null)
            {

                var result = new
                {
                    Id = dbTopic.Id,
                    Course = dbTopic.CourseId,
                    Name = dbTopic.Name,
                    Duration = dbTopic.Duration,
                    Context = dbTopic.Context
                };

                return result;
            }
            return "not found";
        }
        public IEnumerable<Topic> GetAllTopicsByCourseId(int courseId)
        {
            
            try
            {
                return _context.Topics.Where(u => u.CourseId == courseId).Include("Course").ToList();
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

