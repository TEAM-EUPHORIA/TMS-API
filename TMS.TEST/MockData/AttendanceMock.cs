using System.Collections.Generic;
using TMS.BAL;

namespace TMS.TEST
{
 public static class AttendanceMock
        {
            public static List<Attendance> GetAttendances()
            {
                return new List<Attendance>(){
                new Attendance(){
                    CourseId = 1,
                    TopicId = 1,
                    OwnerId = 1

                },
                 new Attendance(){
                    CourseId = 2,
                    TopicId = 2,
                    OwnerId = 2

                },
                 new Attendance(){
                    CourseId = 3,
                    TopicId = 3,
                    OwnerId = 3
                },

            };
            }
            public static Attendance GetAttendance()
            {
                return new Attendance()
                {
                    CourseId = 3,
                    TopicId = 3,
                    OwnerId = 3
                };
            }
        }
}
