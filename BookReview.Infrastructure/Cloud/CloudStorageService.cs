using BookReview.Core.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System.Security.AccessControl;

namespace BookReview.Infrastructure.Cloud
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        private readonly string _baseUrlCloudStorage;
        private readonly IConfiguration _configuration;

        public CloudStorageService(IConfiguration configuration)
        {
            _configuration = configuration;

            var credentialPath = configuration["CloudStorageSettings:CrPath"];

            GoogleCredential credential = GoogleCredential.FromFile(credentialPath);
            _storageClient = StorageClient.Create(credential);

            _bucketName = _configuration["CloudStorageSettings:BucketName"];
            _baseUrlCloudStorage = _configuration["CloudStorageSettings:BaseUrl"];
        }

        public async Task<string> GetBookCoverAsync(string id)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await _storageClient.DownloadObjectAsync(_bucketName, id, memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Google.GoogleApiException ex)
            {
                Console.WriteLine($"Erro ao obter a imagem: {ex.Message}");

                return null;
            }
        }

        public async Task<string> UploadBookCoverAsync(string fileName, MemoryStream memoryStream)
        {
            await _storageClient.UploadObjectAsync(_bucketName, fileName, "image/jpeg", memoryStream);
                       
            return fileName;
        }
    }
}
