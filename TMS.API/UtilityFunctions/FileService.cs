namespace TMS.API
{
    public static partial class FileService
    {
        public static File GetBase64HeaderAndByteArray(string base64String)
        {
            String[] substrings = base64String.Split(',');

            string header = substrings[0];
            string imgData = substrings[1];

            byte[] bytes = Convert.FromBase64String(imgData);

            return new File { header = header, bytes = bytes };
        }
    }
}