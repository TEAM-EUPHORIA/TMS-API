using TMS.BAL;
using Microsoft.EntityFrameworkCore;
namespace TMS.API
{
    public class AppDbContext : DbContext
    {
        #pragma warning disable CS1591
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
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
            var createdOn = DateTime.UtcNow;
            var password = "XIBWXbbynaCwGqElIsN7MvEhy+R6hh738AbLIpIt/6E=";
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
             modelBuilder.Entity<User>()
                .HasData(
                    new User() { EmployeeId = "TMS101", isDisabled = false, CreatedOn = createdOn, Id = 1, UserName = "Warren", Password = password, FullName = "Warren Mackenzie", Email = "Warren.Mackenzie@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 1 },
                    new User() { EmployeeId = "TMS102", isDisabled = false, CreatedOn = createdOn, Id = 2, UserName = "William", Password = password, FullName = "William MacLeod", Email = "William.MacLeod@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 1 },
                    new User() { EmployeeId = "TMS203", isDisabled = false, CreatedOn = createdOn, Id = 3, UserName = "Abigail", Password = password, FullName = "Abigail Manning", Email = "Abigail.Manning@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 2 },
                    new User() { EmployeeId = "TMS204", isDisabled = false, CreatedOn = createdOn, Id = 4, UserName = "Alexandra", Password = password, FullName = "Alexandra Marshall", Email = "Alexandra.Marshall@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 2 },
                    new User() { EmployeeId = "TMS205", isDisabled = false, CreatedOn = createdOn, Id = 5, UserName = "Alison", Password = password, FullName = "Alison Martin", Email = "Alison.Martin@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 2 },
                    new User() { EmployeeId = "TMS316", isDisabled = false, CreatedOn = createdOn, Id = 6, UserName = "Austin", Password = password, FullName = "Austin Bailey", Email = "Austin.Bailey@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 3, DepartmentId = 1 },
                    new User() { EmployeeId = "TMS327", isDisabled = false, CreatedOn = createdOn, Id = 7, UserName = "Benjamin", Password = password, FullName = "Benjamin Baker", Email = "Benjamin.Baker@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 3, DepartmentId = 2 },
                    new User() { EmployeeId = "TMS338", isDisabled = false, CreatedOn = createdOn, Id = 8, UserName = "Blake", Password = password, FullName = "Blake Ball", Email = "Blake.Ball@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 3, DepartmentId = 3 },
                    new User() { EmployeeId = "TMS519", isDisabled = false, CreatedOn = createdOn, Id = 9, UserName = "Ella", Password = password, FullName = "Ella Payne", Email = "Ella.Payne@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 5, DepartmentId = 1 },
                    new User() { EmployeeId = "TMS5210", isDisabled = false, CreatedOn = createdOn, Id = 10, UserName = "Emily", Password = password, FullName = "Emily Peake", Email = "Emily.Peake@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 5, DepartmentId = 2 },
                    new User() { EmployeeId = "TMS5211", isDisabled = false, CreatedOn = createdOn, Id = 11, UserName = "Emma", Password = password, FullName = "Emma Peters", Email = "Emma.Peters@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 5, DepartmentId = 3 },
                    new User() { EmployeeId = "TMS4112", isDisabled = false, CreatedOn = createdOn, Id = 12, UserName = "Charles", Password = password, FullName = "Charles Bower", Email = "Charles.Bower@tms.edu", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 1 },
                    new User() { EmployeeId = "TMS4213", isDisabled = false, CreatedOn = createdOn, Id = 13, UserName = "Christian", Password = password, FullName = "Christian Brown", Email = "Christian.Brown@tms.edu", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 2 },
                    new User() { EmployeeId = "TMS4314", isDisabled = false, CreatedOn = createdOn, Id = 14, UserName = "Christopher", Password = password, FullName = "Christopher Buckland", Email = "Christopher.Buckland@tms.edu", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 3 },
                    new User() { EmployeeId = "TMS4115", isDisabled = false, CreatedOn = createdOn, Id = 15, UserName = "Gabrielle", Password = password, FullName = "Gabrielle Pullman", Email = "Gabrielle.Pullman@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 1 },
                    new User() { EmployeeId = "TMS4216", isDisabled = false, CreatedOn = createdOn, Id = 16, UserName = "Grace", Password = password, FullName = "Grace Quinn", Email = "Grace.Quinn@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 2 },
                    new User() { EmployeeId = "TMS4217", isDisabled = false, CreatedOn = createdOn, Id = 17, UserName = "Hannah", Password = password, FullName = "Hannah Rampling", Email = "Hannah.Rampling@tms.edu.in", Base64 = "data:image/jpeg;base64,", RoleId = 4, DepartmentId = 3 }
                    );
            // Course
            modelBuilder.Entity<Course>()
               .HasData(
                   new Course() { Id = 1, CreatedOn = createdOn, isDisabled = false, DepartmentId = 3, Name = "HTML 5 With CSS 3", Duration = "11 hrs", Description = "You can launch a new career in web development today by learning HTML & CSS. You don't need a computer science degree or expensive software. All you need is a computer, a bit of time, a lot of determination, and a teacher you trust. I've taught HTML and CSS to countless coworkers and held training sessions for fortune 100 companies. I am that teacher you can trust." },
                   new Course() { Id = 2, CreatedOn = createdOn, isDisabled = false, DepartmentId = 3, Name = "SQL RDBMS", Duration = "28 hrs", Description = "You can launch a new career by learning SQL RDBMS. You don't need a computer science degree or expensive software. All you need is a computer, a bit of time, a lot of determination, and a teacher you trust. I've taught RDBMS to countless coworkers and held training sessions for fortune 100 companies. I am that teacher you can trust." }
                   );

                  // CourseUsers
            modelBuilder
                .Entity<CourseUsers>()                
                .HasData(
                    new { CourseId = 1, UserId = 12, RoleId=4, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 1, UserId = 13 ,RoleId=4, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 1, UserId = 14 ,RoleId=4, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 1, UserId = 8 ,RoleId=3, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 2, UserId = 16 ,RoleId=4, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 2, UserId = 17 ,RoleId=4, CreatedOn = createdOn,CreatedBy=3},
                    new { CourseId = 2, UserId = 7,RoleId= 3, CreatedOn = createdOn,CreatedBy=3}
                );
            // Topic
            modelBuilder.Entity<Topic>()
            .HasData(
                new Topic() { TopicId=1,CreatedOn = createdOn, isDisabled = false, CourseId = 1, Name = "HTML Basics", Duration = "50 mins", Content = "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes" },
                new Topic() { TopicId=2,CreatedOn = createdOn, isDisabled = false, CourseId = 1, Name = "CSS Basics", Duration = "50 mins", Content = "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes" },
                new Topic() { TopicId=3,CreatedOn = createdOn, isDisabled = false, CourseId = 1, Name = "HTML & CSS Together", Duration = "50 mins", Content = "All HTML documents must start with a document type declaration: <!DOCTYPE html>. \nThe HTML document itself begins with < html > and ends with </ html >.\nThe visible part of the HTML document is between<> and </ body >.\n HTML headings are defined with the <h1> to <h6> tags. \n< h1 > defines the most important heading. < h6 > defines the least important heading: \nHTML paragraphs are defined with the <p> tag \n HTML links are defined with the <a> tag \nHTML images are defined with the <img> tag.\nThe source file(src), alternative text(alt),width,and height are provided as attributes" },
                new Topic() { TopicId=4,CreatedOn = createdOn, isDisabled = false, CourseId = 2, Name = "RDBMS Basics", Duration = "50 mins", Content = "RDBMS stands for Relational DataBase Management Systems. It is basically a program that allows us to create, delete, and update a relational database. Relational Database is a database system that stores and retrieves data in a tabular format organized in the form of rows and columns.It is a smaller subset of DBMS which was designed by E.F Codd in the 1970s. The major DBMS like SQL, My-SQL, ORACLE are all based on the principles of relational DBMS" },
                new Topic() { TopicId=5,CreatedOn = createdOn, isDisabled = false, CourseId = 2, Name = "ACID Property", Duration = "50 mins", Content = "DBMS is the management of data that should remain integrated when any changes are done in it. It is because if the integrity of the data is affected, whole data will get disturbed and corrupted. Therefore, to maintain the integrity of the data, there are four properties described in the database management system, which are known as the ACID properties. The ACID properties are meant for the transaction that goes through a different group of tasks, and there we come to see the role of the ACID properties.\nIn this section, we will learn and understand about the ACID properties. We will learn what these properties stand for and what does each property is used for. We will also understand the ACID properties with the help of some examples." }
            );
            // Review
            modelBuilder.Entity<Review>()
                    .HasData(
                        new Review() { CreatedOn = createdOn, Id = 1, ReviewerId = 11, StatusId = 1, ReviewDate = DateTime.UtcNow , ReviewTime = DateTime.UtcNow, TraineeId = 13, Mode = "online" ,DepartmentId=2},
                        new Review() { CreatedOn = createdOn, Id = 2, ReviewerId = 11, StatusId = 2, ReviewDate = DateTime.UtcNow, ReviewTime = DateTime.UtcNow, TraineeId = 15, Mode = "Offline",DepartmentId=1 },
                        new Review() { CreatedOn = createdOn, Id = 3, ReviewerId = 11, StatusId = 3, ReviewDate = DateTime.UtcNow, ReviewTime = DateTime.UtcNow, TraineeId = 16, Mode = "online",DepartmentId=2 },
                        new Review() { CreatedOn = createdOn, Id = 4, ReviewerId = 10, StatusId = 1, ReviewDate = DateTime.UtcNow, ReviewTime = DateTime.UtcNow, TraineeId = 12, Mode = "online",DepartmentId=1 },
                        new Review() { CreatedOn = createdOn, Id = 5, ReviewerId = 9, StatusId = 2, ReviewDate = DateTime.UtcNow, ReviewTime = DateTime.UtcNow, TraineeId = 12, Mode = "Offline" ,DepartmentId=1},
                        new Review() {  CreatedOn = createdOn, Id = 6, ReviewerId = 10, StatusId = 3, ReviewDate = DateTime.UtcNow, ReviewTime = DateTime.UtcNow, TraineeId = 17, Mode = "online",DepartmentId=3 }
                        );
            // MOM
            modelBuilder.Entity<MOM>()
                    .HasData(
                        new MOM() { CreatedOn = createdOn, ReviewId = 1, TraineeId = 13, Agenda = "Lorem ipsum dolor sit amet\nAdipiscing elit duis\nAenean euismod elementum", MeetingNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dolor purus non enim praesent elementum facilisis. Nisi scelerisque eu ultrices vitae auctor eu augue ut lectus. Adipiscing elit duis tristique sollicitudin nibh sit amet. Id eu nisl nunc mi ipsum faucibus vitae aliquet. Cras ornare arcu dui vivamus arcu felis bibendum ut tristique. Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat. Ut ornare lectus sit amet est placerat in egestas erat. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Ac auctor augue mauris augue. Sagittis orci a scelerisque purus semper eget. Massa placerat duis ultricies lacus sed turpis.", PurposeOfMeeting = "Eu consequat ac felis donec et odio pellentesque diam volutpat. Suspendisse in est ante in. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo. Leo vel orci porta non pulvinar neque. Ultrices vitae auctor eu augue ut lectus arcu. Nec sagittis aliquam malesuada bibendum arcu vitae elementum curabitur vitae. Quisque sagittis purus sit amet volutpat. Semper feugiat nibh sed pulvinar proin gravida hendrerit lectus a." },
                        new MOM() { CreatedOn = createdOn, ReviewId = 4, TraineeId=13, Agenda = "Lorem ipsum dolor sit amet\nAdipiscing elit duis\nAenean euismod elementum", MeetingNotes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dolor purus non enim praesent elementum facilisis. Nisi scelerisque eu ultrices vitae auctor eu augue ut lectus. Adipiscing elit duis tristique sollicitudin nibh sit amet. Id eu nisl nunc mi ipsum faucibus vitae aliquet. Cras ornare arcu dui vivamus arcu felis bibendum ut tristique. Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat. Ut ornare lectus sit amet est placerat in egestas erat. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Ac auctor augue mauris augue. Sagittis orci a scelerisque purus semper eget. Massa placerat duis ultricies lacus sed turpis.", PurposeOfMeeting = "Eu consequat ac felis donec et odio pellentesque diam volutpat. Suspendisse in est ante in. Dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo. Leo vel orci porta non pulvinar neque. Ultrices vitae auctor eu augue ut lectus arcu. Nec sagittis aliquam malesuada bibendum arcu vitae elementum curabitur vitae. Quisque sagittis purus sit amet volutpat. Semper feugiat nibh sed pulvinar proin gravida hendrerit lectus a." }
                        );

            //CourseFeedback
            modelBuilder.Entity<CourseFeedback>()
                .HasData(
                    new CourseFeedback() { TraineeId = 13, CourseId = 1, CreatedOn = createdOn, Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", Rating = 3.8f },
                    new CourseFeedback() { TraineeId = 17, CourseId = 2, CreatedOn = createdOn, Feedback = " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", Rating = 4.3f },
                    new CourseFeedback() { TraineeId = 12, CourseId = 1, CreatedOn = createdOn, Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", Rating = 4.8f },
                    new CourseFeedback() { TraineeId = 16, CourseId = 2, CreatedOn = createdOn, Feedback = " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", Rating = 3.6f }
                    );
            //TraineeFeedback
            modelBuilder.Entity<TraineeFeedback>()
                .HasData(
                    new TraineeFeedback() {  TraineeId = 13, CourseId = 1, CreatedOn = createdOn,  Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", TrainerId = 8 },
                    new TraineeFeedback() {  TraineeId = 17, CourseId = 2, CreatedOn = createdOn,  Feedback = " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis", TrainerId = 7 },
                    new TraineeFeedback() {  TraineeId = 12, CourseId = 1, CreatedOn = createdOn,  Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu ", TrainerId = 8 },
                    new TraineeFeedback() {  TraineeId = 16, CourseId = 2, CreatedOn = createdOn,  Feedback = " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu", TrainerId = 7 }
                    );

        }  
    }
}
