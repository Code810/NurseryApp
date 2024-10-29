

using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Extensions
{
    public static class FileExtension
    {
        public static string Save(this IFormFile file, string root, string folder)
        {
            string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string directoryPath = Path.Combine(root, "wwwroot", folder);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }
    }
}
