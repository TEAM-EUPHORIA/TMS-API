using TMS.BAL;
using Microsoft.EntityFrameworkCore;
namespace TMS.API
{
    public class AppDbContext : DbContext
    {
        #pragma warning disable CS8618
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        #pragma warning restore CS8618
         public static DbContextOptionsBuilder<AppDbContext> dbContextOptions(){
         var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("Default");
            
                optionsBuilder.UseSqlServer(connectionString
                                         ?? throw new NullReferenceException(
                                             $"Connection string is passed as null {nameof(connectionString)}"));
            return optionsBuilder;
        }
        // main entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        // dependent entites
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CourseFeedback> CourseFeedbacks { get; set; }
        public DbSet<TraineeFeedback> TraineeFeedbacks { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MOM> MOMs { get; set; }
        public DbSet<ReviewStatus> ReviewStatuses { get; set; }
        // mapping entity
        public DbSet<CourseUsers> CourseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>().HasKey(e=>new{e.CourseId,e.TopicId,e.OwnerId});
            modelBuilder.Entity<Attendance>().HasKey(e=>new{e.CourseId,e.TopicId,e.OwnerId});
            modelBuilder.Entity<CourseUsers>().HasKey(e=>new{e.CourseId,e.UserId,e.RoleId});
            modelBuilder.Entity<CourseFeedback>().HasKey(e=>new{e.CourseId,e.TraineeId});
            modelBuilder.Entity<TraineeFeedback>().HasKey(e=>new{e.CourseId,e.TraineeId,e.TrainerId});
            modelBuilder.Entity<Review>().HasKey(e=>new{e.Id});
            modelBuilder.Entity<MOM>().HasKey(e=>new{e.ReviewId,e.TraineeId});
            modelBuilder.Entity<Role>().HasData(
                new Role(){Id=1,Name="Training Head",CreatedOn=DateTime.Now,isDisabled=false},
                new Role(){Id=2,Name="Training Coordinator",CreatedOn=DateTime.Now,isDisabled=false},
                new Role(){Id=3,Name="Trainer",CreatedOn=DateTime.Now,isDisabled=false},
                new Role(){Id=4,Name="Trainee",CreatedOn=DateTime.Now,isDisabled=false},
                new Role(){Id=5,Name="Reviewer",CreatedOn=DateTime.Now,isDisabled=false}
            );
            modelBuilder.Entity<Department>().HasData(
                new Department(){Id=1,Name=".NET",CreatedOn=DateTime.Now,isDisabled=false},
                new Department(){Id=2,Name="JAVA",CreatedOn=DateTime.Now,isDisabled=false},
                new Department(){Id=3,Name="LAMP",CreatedOn=DateTime.Now,isDisabled=false}
            );
            modelBuilder.Entity<ReviewStatus>().HasData(
                new ReviewStatus(){Id=1,Name="Assigned",CreatedOn=DateTime.Now},
                new ReviewStatus(){Id=2,Name="Completed",CreatedOn=DateTime.Now},
                new ReviewStatus(){Id=3,Name="Canceled",CreatedOn=DateTime.Now}
            );
        }  
    }
}
