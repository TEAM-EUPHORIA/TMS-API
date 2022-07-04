using TMS.API.UtilityFunctions;
using TMS.BAL;

namespace TMS.API.Services
{
    public partial class UserService
    {
        private void GenerateUserId(User user)
        {
            var count  = _repo.Stats.GetUserCount();
            int newId = 1;
            if(count > 0)
            {
                newId = _stats.lastUserId() + 1;
            }
            if (user.DepartmentId != 0 && user.DepartmentId != null) user.EmployeeId = $"TMS{user.RoleId}{user.DepartmentId}{newId}";
            else user.EmployeeId = $"TMS{user.RoleId}0{newId}";
        }
        private void SetUpImage(User user)
        {
            File Image = FileService.GetBase64HeaderAndByteArray(user.Base64);
            user.Base64 = Image.header;
            user.Image = Image.bytes;
        }
        private void SetUpUserDetails(User user)
        {
            user.isDisabled = false;
            user.Password = HashPassword.Sha256(user.Password);
            if (string.IsNullOrEmpty(user.EmployeeId)) GenerateUserId(user);
            if (!string.IsNullOrEmpty(user.Base64) && user.Base64.Length > 1000) SetUpImage(user);
            user.CreatedOn = DateTime.Now;
        }
        private void SetUpUserDetails(User user, User dbUser)
        {
            dbUser.isDisabled = false;
            dbUser.Password = HashPassword.Sha256(user.Password);
            dbUser.FullName = user.FullName;
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            SetUpImage(user);
            dbUser.Base64 = user.Base64;
            dbUser.Image = user.Image;
            dbUser.UpdatedOn = DateTime.Now;
            if(user.DepartmentId != 0) dbUser.DepartmentId = user.DepartmentId;
        }
        
        private void disable(int currentUserId, User dbUser)
        {
            dbUser.isDisabled = true;
            dbUser.UpdatedBy = currentUserId;
            dbUser.UpdatedOn = DateTime.Now;
        }
    }
}