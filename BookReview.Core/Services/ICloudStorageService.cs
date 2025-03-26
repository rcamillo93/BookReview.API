using System.Buffers.Text;

namespace BookReview.Core.Services
{
    public interface ICloudStorageService
    {
        Task<string> UploadBookCoverAsync(string fileName, MemoryStream MemoryStream);
        Task<string> GetBookCoverAsync(string id);
    }
}
