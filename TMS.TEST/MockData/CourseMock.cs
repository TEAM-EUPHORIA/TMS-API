using System.Collections.Generic;
using TMS.API.ViewModels;
using TMS.BAL;

namespace TMS.TEST
{
    public static class CourseMock
    {
        public static List<Course> GetCourses()
        {
            return new List<Course>(){
                new Course(){
                    Id = 1,
                    Name = "C#"
                },
                new Course(){
                    Id = 2,
                    Name = "C++"
                },
                new Course(){
                    Id = 3,
                    Name = "Python"
                }
            };

        }
        public static Course GetCourse()
        {
            return new Course()
            {
                Id = 1,
                Name = "C#"
            };

        }
        public static AddUsersToCourse GetData()
        {
            return new AddUsersToCourse()
            {
                CourseId=1,
                users=new List<CourseUser>(){
                    new CourseUser(){
                        UserId = 1,
                        RoleId = 4
                    },  
                    new CourseUser(){
                        UserId = 2,
                        RoleId = 4
                    },  
                    new CourseUser(){
                        UserId = 3,
                        RoleId = 4
                    }, 
                    new CourseUser(){
                        UserId = 4,
                        RoleId = 4
                    }  
                }
            };         
        }
        public static List<CourseUsers> GetResult()
        {
            return new List<CourseUsers>(){
                new CourseUsers(){
                    CourseId = 1,
                    UserId = 1,
                    RoleId = 4                     
                },
                new CourseUsers(){
                    CourseId = 1,
                    UserId = 2,
                    RoleId = 4                     
                },
                new CourseUsers(){
                    CourseId = 1,
                    UserId = 3,
                    RoleId = 4                     
                }
            };
        }

        
       
    }
}