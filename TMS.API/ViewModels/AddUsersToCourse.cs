namespace TMS.API.ViewModels
{
    public class AddUsersToCourse
    {
        public int CourseId { get; set; }
        public List<CourseUser> users { get; set; } 
    }

    public class CourseUser
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}