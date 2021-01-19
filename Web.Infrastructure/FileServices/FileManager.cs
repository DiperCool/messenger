using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Infrastructure.FileServices
{
    public class FileManager : IFileManager
    {
        public async Task UploadFile(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task UploadFiles(IFormFileCollection files,string path)
        {
            foreach(var file in files)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }   
    }
}