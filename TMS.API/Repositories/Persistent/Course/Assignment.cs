using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class CourseRepository : ICourseRepository
    {
        public void CreateAssignment(Assignment assignment)
        {
            dbContext.Assignments.Add(assignment);
        }
        public Assignment GetAssignmentByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            return dbContext.Assignments.Where(a =>
                    a.CourseId == courseId &&
                    a.TopicId == topicId &&
                    a.OwnerId == ownerId
                ).Include(a => a.Owner).FirstOrDefault()!;
        }
        public IEnumerable<Assignment> GetAssignmentsByTopicId(int topicId)
        {
            return dbContext.Assignments
                    .Where(a => a.TopicId == topicId && a.Owner.RoleId == 4)
                    .Include(a => a.Owner);
        }
        public void UpdateAssignment(Assignment assignment)
        {
            dbContext.Assignments.Update(assignment);
        }
    }
}