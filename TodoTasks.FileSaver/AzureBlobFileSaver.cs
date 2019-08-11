using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.FileSaver
{
    public class AzureBlobFileSaver : IFileSaver
    {
        private readonly IConfiguration _configuration;

        public AzureBlobFileSaver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileSaver, AzureBlobFileSaver>();
        }

        public async Task DeleteFile(string filename)
        {
            var container = GetBlobContainer();
            var blob = container.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public Task<string> GetFilePath(string filename)
        {
            var container = GetBlobContainer();
            var blob = container.GetBlockBlobReference(filename);

            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(1),
            };
            var headers = new SharedAccessBlobHeaders()
            {
                ContentDisposition = string.Format("attachment;filename=\"{0}\"", blob.Name),
            };

            var sasToken = blob.GetSharedAccessSignature(policy, headers);

            return Task.FromResult(blob.Uri.AbsoluteUri + sasToken);
        }

        public async Task SaveFile(string filename, Stream stream)
        {
            var container = GetBlobContainer();
            var newBlob = container.GetBlockBlobReference(filename);
            await newBlob.UploadFromStreamAsync(stream);
        }

        private CloudBlobContainer GetBlobContainer()
        {
            var storageCredentials = new StorageCredentials(_configuration["StorageAccountName"], _configuration["StorageKey"]);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return cloudBlobClient.GetContainerReference(_configuration["StorageContainerName"]);
        }
    }
}
