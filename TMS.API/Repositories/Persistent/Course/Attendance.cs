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
            var result = dbContext.Attendances.Where(a => 
                    a.CourseId == courseId && 
                    a.TopicId == topicId
                ).Include(a => a.Owner).ToList();

            var userIds = result.Select(a=>a.OwnerId);
            var courseUsers = dbContext.CourseUsers
                                .Where(cu => cu.CourseId == courseId)
                                .Include(cu => cu.User)
                                .Select(cu => cu.User).ToList();

            var AttendanceExists = false;
            foreach (var item in courseUsers)
            {
                AttendanceExists = dbContext.Attendances.Any(a=>a.CourseId == courseId && a.TopicId == topicId && a.OwnerId == item!.Id);
                if(!AttendanceExists)
                {
                    var attendance = new Attendance
                    {
                        CourseId = courseId,
                        TopicId = topicId,
                        OwnerId = item!.Id,
                        Owner = item,
                        Status = false
                    };
                    result.Add(attendance);
                }
            }
            return result;
        }
    }
}