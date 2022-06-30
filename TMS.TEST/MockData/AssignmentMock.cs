using System.Collections.Generic;
using TMS.BAL;

namespace TMS.TEST
{
    public static class AssignmentMock
    {
        
        public static List<Assignment> GetAssignmentsByTopicId()
        {
            return new List<Assignment>(){
                new Assignment(){
                    TopicId = 1
                },
            };
        }
        public static Assignment GetAssignment()
        {
            return new Assignment(){
                    TopicId = 3,
                    CourseId = 1,
                    OwnerId = 1,
                    
                };
        }
        public static Assignment GetAssignments()
        {
            return new Assignment(){
                CourseId = 1,
                TopicId = 3,
                OwnerId = 1
            };

        }
        public static int GetAssignmentByTopicId()
        {
            return 1;

        }

       
    }
}
