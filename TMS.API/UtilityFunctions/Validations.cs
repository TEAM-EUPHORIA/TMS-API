using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMS.BAL;

namespace TMS.API
{
    public static class Validation
    {
        private static void AddErrors(Dictionary<string, string> result, string propertyName)
        {
            result.Add($"{propertyName}", $"Invalid data.");
        }
        public static Dictionary<string, string> ValidateUser(User user)
        {
            var result = new Dictionary<string, string>();

            var emailValidation = new Regex(@"^([0-9a-zA-Z.]){3,}@[a-zA-z]{3,}(.[a-zA-Z]{2,}[a-zA-Z]*){0,}$");
            var fullNameValidation = new Regex(@"^(?!([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            var userNameValidation = new Regex(@"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$");
            var passwordValidation = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            var base64Validation = new Regex(@"^data:image\/[a-zA-Z]+;base64,");

            if (!userNameValidation.IsMatch(user.UserName)) AddErrors(result, nameof(user.UserName));
            if (!fullNameValidation.IsMatch(user.FullName)) AddErrors(result, nameof(user.FullName));
            if (!emailValidation.IsMatch(user.FullName)) AddErrors(result, nameof(user.Email));
            if (!passwordValidation.IsMatch(user.UserName)) AddErrors(result, nameof(user.Password));
            if (user.RoleId <= 0 || user.RoleId > 6) AddErrors(result, nameof(user.RoleId));
            if (user.DepartmentId != null && (user.DepartmentId <= 0 || user.DepartmentId > 3)) AddErrors(result, nameof(user.DepartmentId));
            if (!base64Validation.Match(user.Base64).Success) AddErrors(result, nameof(user.Base64));

            if (result.Count == 0) result.Add("IsValid", "true");

            return result;
        }

    }
}