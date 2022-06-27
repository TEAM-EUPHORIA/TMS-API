using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class TopicMock
    {
        public static List<Topic> GetTopics()
        {
            return new List<Topic>(){
                new Topic(){
                    TopicId= 1,
                    Name="OOPS",
                    CourseId = 1
                },
                new Topic(){
                    TopicId= 2,
                    Name="Exception Handling",
                    CourseId = 1
                },
                new Topic(){
                    TopicId= 3,
                    Name="Angular",
                    CourseId = 1
                },


            };
        }
        public static Topic GetTopic()
        {
            return 
                new Topic(){
                    TopicId= 3,
                    Name="Angular",
                    CourseId = 1
                };

        }
    }
}