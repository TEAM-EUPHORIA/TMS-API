using Microsoft.EntityFrameworkCore;
using TMS.API.Models;

namespace TMS.API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics{get; set;}
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceStatus> AttendanceStatuses { get; set; }
        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public DbSet<CourseFeedback> CourseFeedbacks { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewStatus> ReviewStatuses { get; set; }
        public DbSet<MOM> MOMs { get; set; }
        public DbSet<MOMStatus> MOMStatuses { get; set; }

        public DbSet<TraineeFeedback> traineeFeedbacks { get; set; }
    }
}
