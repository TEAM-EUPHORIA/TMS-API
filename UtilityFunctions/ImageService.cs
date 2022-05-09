using System.Drawing;

namespace TMS.API.UtilityFunctions
{
    public static class ImageService
    {
        public static byte[] imageToByteArray(IFormFile imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
    }
}