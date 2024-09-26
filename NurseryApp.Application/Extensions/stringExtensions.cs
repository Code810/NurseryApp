namespace NurseryApp.Application.Extensions
{
    public static class stringExtensions
    {
        public static bool IsImage(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp" || extension == ".svg";
        }
    }
}
