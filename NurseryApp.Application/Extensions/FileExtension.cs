

using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Extensions
{
    public static class FileExtension
    {
        public static string Save(this IFormFile file, string root, string folder)
        {
            // Generate a unique file name
            string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string directoryPath = Path.Combine(root, "wwwroot", folder);

            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Complete path for the new file
            string filePath = Path.Combine(directoryPath, newFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }
    }
}
