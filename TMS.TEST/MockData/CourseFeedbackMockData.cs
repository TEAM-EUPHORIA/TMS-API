using System.Collections.Generic;
using TMS.BAL;
namespace UnitTesting.MockData{
    public static class CoursefeedbackMock{
        public static List<CourseFeedback>GetCourseFeedbacksMock(){
            List<CourseFeedback>MockCourseFeedback=new List<CourseFeedback>();
            MockCourseFeedback.Add(new CourseFeedback{TraineeId = 13, CourseId = 1, Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", Rating = 3.8f});
            MockCourseFeedback.Add(new CourseFeedback{TraineeId = 17,  CourseId = 2, Feedback = " Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", Rating = 4.3f });
            MockCourseFeedback.Add(new CourseFeedback{ TraineeId = 12, CourseId = 3, Feedback = "Enim lobortis scelerisque fermentum dui faucibus in ornare quam viverra. Dui vivamus arcu felis bibendum ut. Enim blandit volutpat maecenas volutpat", Rating = 4.8f });
            return MockCourseFeedback;

        }
        }
    }