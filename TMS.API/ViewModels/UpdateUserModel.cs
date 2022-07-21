namespace TMS.API.ViewModels
{
    public class UpdateUserModel
    {
       
       public int Id { get; set; }
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Base64 { get; set; }
        public byte[]? Image { get; set; }
    }
}