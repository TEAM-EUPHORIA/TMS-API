using Microsoft.EntityFrameworkCore;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class CourseService
    {
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId,AppDbContext dbContext)
        {
            var topicExists = Validation.TopicExists(dbContext,topicId);            
            if(topicExists)
            {
                var result = dbContext.Topics.Where(t => t.TopicId == topicId!)
                    .Include(t => t.Assignments!).ThenInclude(a => a.Owner!).ThenInclude(u => u.Role)
                    .Include(t => t.Assignments).FirstOrDefault();
        
                if(result is not null && result.Assignments is not null)return result.Assignments;
            } 
            throw new ArgumentException("Invalid Id");
        }
        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId,int topicId, int ownerId,AppDbContext dbContext)
        {
            var assignmentExists = Validation.AssignmentExists(dbContext,courseId,topicId,ownerId);
            if(assignmentExists)
            {
                var result = dbContext.Assignments.Where(a => a.TopicId == topicId && a.OwnerId == ownerId).FirstOrDefault();
                if(result is not null) return result;
            }
            throw new ArgumentException("Invalid Id's");
        }
        public Dictionary<string, string> CreateAssignment(Assignment assignment,AppDbContext dbContext)
        {
            if (assignment is null) throw new ArgumentNullException(nameof(assignment));
            var validation = Validation.ValidateAssignment(assignment,dbContext);
            if (validation.ContainsKey("IsValid") && !validation.ContainsKey("Exists"))
            {
                prepareAssignment(assignment);
                dbContext.Add(assignment);
                dbContext.SaveChanges();                   
            }
            return validation;
        }
        public Dictionary<string, string> UpdateAssignment(Assignment assignment,AppDbContext dbContext)
        {
            if (assignment is null) throw new ArgumentNullException(nameof(assignment));
            var validation = Validation.ValidateAssignment(assignment,dbContext);
            if (validation.ContainsKey("IsValid") && validation.ContainsKey("Exists"))
            {    
                var dbAssignment = dbContext.Assignments.Where(a=>a.CourseId==assignment.CourseId && a.TopicId == assignment.TopicId && a.OwnerId == assignment.OwnerId).FirstOrDefault();           
                if(dbAssignment is not null)
                {
                    prepareAssignment(assignment, dbAssignment);
                    dbContext.Update(dbAssignment);
                    dbContext.SaveChanges();                      
                }
            }
            return validation;
        }
    }
}