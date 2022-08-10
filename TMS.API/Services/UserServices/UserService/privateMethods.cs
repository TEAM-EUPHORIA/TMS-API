using TMS.API.UtilityFunctions;
using TMS.API.ViewModels;
using TMS.BAL;
namespace TMS.API.Services
{
    public partial class UserService
    {
        /// <summary>
        /// generate Employee id for the user while creating it.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void GenerateEmployeeId(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var count = _repo.Stats.GetUserCount();
            int newId = 1;
            if (count > 0)
            {
                newId = _stats.LastUserId() + 1;
            }
            if (user.DepartmentId != 0 && user.DepartmentId != null) user.EmployeeId = $"TMS{user.RoleId}{user.DepartmentId}{newId}";
            else user.EmployeeId = $"TMS{user.RoleId}0{newId}";
        }
        /// <summary>
        /// used to split the meta date and base64 string. 
        /// The base64 strings is converted to byte[]. 
        /// used for setting up Image in user model
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpImage(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            File Image = FileService.GetBase64HeaderAndByteArray(user.Base64!);
            user.Base64 = Image.Header!;
            user.Image = Image.Bytes;
        }
        private static void SetUpImage(UpdateUserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            File Image = FileService.GetBase64HeaderAndByteArray(user.Base64!);
            user.Base64 = Image.Header!;
            user.Image = Image.Bytes;
        }
        /// <summary>
        /// used to setup the user model for creating.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createdBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void SetUpUserDetailsForCreate(User user, int createdBy)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.isDisabled = false;
            user.Password = HashPassword.Sha256(user.Password!);
            if (string.IsNullOrEmpty(user.EmployeeId)) GenerateEmployeeId(user);
            if (!string.IsNullOrEmpty(user.Base64) && user.Base64.Length > 1000) SetUpImage(user);
            user.CreatedOn = DateTime.Now;
            user.CreatedBy = createdBy;
        }
        /// <summary>
        /// used to setup the user model for update.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dbUser"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpUserDetailsForUpdate(User user, User dbUser, int updateBy)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (dbUser is null)
            {
                throw new ArgumentNullException(nameof(dbUser));
            }
            dbUser.Password = HashPassword.Sha256(user.Password!);
            if (dbUser.FullName != user.FullName)
                dbUser.FullName = user.FullName;
            if (dbUser.UserName != user.UserName)
                dbUser.UserName = user.UserName;
            if (dbUser.Email != user.Email)
                dbUser.Email = user.Email;
            SetUpImage(user);
            dbUser.Base64 = user.Base64;
            dbUser.Image = user.Image;
            dbUser.UpdatedOn = DateTime.Now;
            dbUser.UpdatedBy = updateBy;
            if (user.DepartmentId != 0 && user.DepartmentId != null) dbUser.DepartmentId = user.DepartmentId;
        }
        /// <summary>
        /// used to setup the user model for update.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dbUser"></param>
        /// <param name="updateBy"></param>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void SetUpUserDetailsForUpdate(UpdateUserModel user, User dbUser, int updateBy)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (dbUser is null)
            {
                throw new ArgumentNullException(nameof(dbUser));
            }
            if (dbUser.FullName != user.FullName)
                dbUser.FullName = user.FullName;
            if (dbUser.UserName != user.UserName)
                dbUser.UserName = user.UserName;
            if (dbUser.Email != user.Email)
                dbUser.Email = user.Email;
            SetUpImage(user);
            dbUser.Base64 = user.Base64;
            dbUser.Image = user.Image;
            dbUser.UpdatedOn = DateTime.Now;
            dbUser.UpdatedBy = updateBy;
            if (user.DepartmentId != 0 && user.DepartmentId != null) dbUser.DepartmentId = user.DepartmentId;
        }
        /// <summary>
        /// used to disable the user.
        /// </summary>
        /// <param name="dbUser"></param>
        /// <param name="updatedBy"></param>
        private static void Disable(User dbUser, int updatedBy)
        {
            dbUser.isDisabled = true;
            dbUser.UpdatedBy = updatedBy;
            dbUser.UpdatedOn = DateTime.Now;
        }
    }
}