namespace TMS.API.UtilityFunctions
{
    public static class ImageService
    {
        public static string Getbase64String(string value)
        {
            if (value.Contains("jpeg"))
            {
                return value.Replace("data:image/jpeg;base64,", "");
            }
            else if (value.Contains("jpg"))
            {
                return value.Replace("data:image/jpg;base64,", "");
            }
            else
            {
                return value.Replace("data:image/png;base64,", "");
            }
        }
        public static string Getbase64Header(string value)
        {
            if (value.Contains("jpeg"))
            {
                return "data:image/jpeg;base64,";
            }
            else if (value.Contains("jpg"))
            {
                return "data:image/jpg;base64,";
            }
            else
            {
                return "data:image/png;base64,";
            }
        }
    }
}