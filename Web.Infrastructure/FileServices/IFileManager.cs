using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web.Infrastructure.FileServices
{
    public interface IFileManager
    {
        Task UploadFile(IFormFile file, string path);
        Task UploadFiles(IFormFileCollection files,string path);
    }
}