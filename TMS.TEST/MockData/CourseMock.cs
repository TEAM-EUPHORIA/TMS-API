using System.Collections.Generic;
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
    }
}