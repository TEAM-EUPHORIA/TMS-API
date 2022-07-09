using Microsoft.EntityFrameworkCore;
using TMS.BAL;


namespace TMS.API.Repositories
{
    public partial class CourseRepository : ICourseRepository
    {
        public Attendance GetAttendanceByCourseIdTopicIdAndOwnerId(int courseId, int topicId, int ownerId)
        {
            return dbContext.Attendances.Where(a =>
                    a.CourseId == courseId &&
                    a.TopicId == topicId &&
                    a.OwnerId == ownerId
                ).Include(a => a.Owner).FirstOrDefault()!;
        }
        public void MarkAttendance(Attendance attendance)
        {
            dbContext.Attendances.Add(attendance);
        }
        public IEnumerable<Attendance> GetAttendanceList(int courseId, int topicId)
        {
            return dbContext.Attendances.Where(a => 
                    a.CourseId == courseId && 
                    a.TopicId == topicId
                ).Include(a => a.Owner).ToList();
        }
    }
}