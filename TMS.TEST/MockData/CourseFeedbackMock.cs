using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class CourseFeedbackMock
    {
        public static List<CourseFeedback> GetCourseFeedbacks()
        {
            return new List<CourseFeedback>(){
                new CourseFeedback(){
                    CourseId= 1,
                    TraineeId= 1,
                    Feedback="Romila has had a positive attitude about working overtime to meet a client's needs. In your feedback, show how much you appreciate her extra effort.",
                    Rating =5
                },
               
                new CourseFeedback(){
                    CourseId= 2,
                    TraineeId= 2,
                    Feedback="Ava finished all her work on time and paid attention to the details. Provide positive feedback to keep her motivated.",
                    Rating =4
                },
                 new CourseFeedback(){
                    CourseId= 3,
                    TraineeId=3,
                    Feedback="You have been doing a great job lately. Thank you for being so flexible with projects and working hard to support your team members.",
                    Rating =3
                },
            };
        }
        public static CourseFeedback GetCourseFeedback()
        {
            return new CourseFeedback(){
                    CourseId= 1,
                    TraineeId= 1,
                    Feedback="Romila has had a positive attitude about working overtime to meet a client's needs. In your feedback, show how much you appreciate her extra effort.",
                    Rating =5
                };
        }
    }
}