namespace TMS.API
{
    public static partial class FileService
    {
        /// <summary>
        /// used to split header and data from base64 string
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns>
        /// file with header string and data byte[]
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="FormatException">
        /// </exception>
        public static File GetBase64HeaderAndByteArray(string base64String)
        {
            String[] substrings = base64String.Split(',');

            string header = substrings[0];
            string imgData = substrings[1];

            byte[] bytes = Convert.FromBase64String(imgData);

            return new File { Header = header, Bytes = bytes };
        }
    }
}