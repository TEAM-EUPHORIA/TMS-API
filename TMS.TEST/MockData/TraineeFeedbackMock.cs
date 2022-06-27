using System.Collections.Generic;
using TMS.BAL;
namespace TMS.TEST
{
    public static class TraineeFeedbackMock
    {
        public static List<TraineeFeedback> GetTraineeFeedbacks()
        {
            return new List<TraineeFeedback>(){
                new TraineeFeedback(){
                    CourseId= 1,
                    TraineeId= 1,
                    TrainerId= 1,
                    Feedback="Romila has had a positive attitude about working overtime to meet a client's needs. In your feedback, show how much you appreciate her extra effort."
                },
                new TraineeFeedback(){
                    CourseId= 2,
                    TraineeId= 2,
                    TrainerId= 2,
                    Feedback="Ava finished all her work on time and paid attention to the details. Provide positive feedback to keep her motivated."
                },
                new TraineeFeedback(){
                    CourseId= 3,
                    TraineeId= 3,
                    TrainerId= 3,
                    Feedback="You have been doing a great job lately. Thank you for being so flexible with projects and working hard to support your team members."
                },
            };
        }
        public static TraineeFeedback GetTraineeFeedback()
        {
            return new TraineeFeedback()
            {
                    CourseId= 3,
                    TraineeId= 3,
                    TrainerId= 3,
                    Feedback="You have been doing a great job lately. Thank you for being so flexible with projects and working hard to support your team members."
            };
        }
    }
}