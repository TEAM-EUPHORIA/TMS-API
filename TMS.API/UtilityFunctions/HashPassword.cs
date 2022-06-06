using System.Security.Cryptography;
using System.Text;

namespace TMS.API.UtilityFunctions
{
    public static class HashPassword
    {
        public static string Sha256(this string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}