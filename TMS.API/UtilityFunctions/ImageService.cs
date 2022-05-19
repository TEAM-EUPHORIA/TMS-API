namespace TMS.API
{
    public static partial class ImageService
    {
        public static Image GetBase64HeaderAndByteArray(string base64String)
        {
            String[] substrings = base64String.Split(',');

            string header = substrings[0];
            string imgData = substrings[1];

            byte[] bytes = Convert.FromBase64String(imgData);

            return new Image { header = header, bytes = bytes };
        }
    }
}