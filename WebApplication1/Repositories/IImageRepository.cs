using Microsoft.AspNetCore.Http.Metadata;

namespace WebApplication1.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
