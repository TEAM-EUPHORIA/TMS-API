using Microsoft.EntityFrameworkCore;
using TMS.BAL;
namespace TMS.API.Repositories
{
    public partial class CourseRepository : ICourseRepository
    {
        public void CreateTopic(Topic topic)
        {
            dbContext.Topics.Add(topic);
        }
        public Topic GetTopicById(int courseId, int topicId, int userId)
        {
            var result = dbContext.Topics
                            .Where(t => t.CourseId == courseId &&
                                        t.TopicId == topicId)
                            .Include(a => a.Attendances)
                            .FirstOrDefault();
            var trainerId = dbContext.CourseUsers
                                .Where(cu => cu.CourseId == courseId &&
                                             cu.RoleId == 3)
                                .FirstOrDefault()!
                                .UserId;
            var assignment = GetAssignmentByCourseIdTopicIdAndOwnerId(courseId, topicId, trainerId);
            var present = dbContext.Assignments.Any(a => a.CourseId == courseId &&
                                                         a.TopicId == topicId &&
                                                         a.OwnerId == userId);
            result!.Assignments = new List<Assignment>() { assignment! };
            if (present)
            {
                var attendance = GetAttendanceByCourseIdTopicIdAndOwnerId(courseId, topicId, userId);
                result.Attendances = new List<Attendance>
                {
                    attendance!
                };
            }
            return result;
        }
        public Topic GetTopicById(int courseId, int topicId)
        {
            var result = dbContext.Topics
                            .Where(t => t.CourseId == courseId &&
                                        t.TopicId == topicId &&
                                        t.isDisabled == false)
                            .FirstOrDefault();
            return result!;
        }
        public IEnumerable<Topic> GetTopicsByCourseId(int courseId)
        {
            return dbContext.Topics
                    .Where(t => t.CourseId == courseId &&
                                t.isDisabled == false).Include(t => t.Assignments).Include(t=>t.Attendances);
        }
        public void UpdateTopic(Topic topic)
        {
            dbContext.Topics.Update(topic);
        }
    }
}