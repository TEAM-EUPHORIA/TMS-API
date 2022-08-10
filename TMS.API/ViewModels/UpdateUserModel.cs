using System.ComponentModel.DataAnnotations;

namespace TMS.API.ViewModels
{
    public class UpdateUserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? FullName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string? UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? Base64 { get; set; }
        public byte[]? Image { get; set; }
    }
}