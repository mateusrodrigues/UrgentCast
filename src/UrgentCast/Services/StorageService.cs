using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrgentCast.Services
{
    public class StorageService : IStorageService
    {
        private CloudBlobContainer _container;

        private readonly string CONTAINER_NAME = "urgentcast";
        private readonly string EPISODES_FOLDER = "episodes";
        private readonly IConfiguration _configuration;

        public StorageService (IConfiguration configuration)
        {
            _configuration = configuration;
            Connect();
        }

        private void Connect()
        {
            // Gets the connection string
            var connString = _configuration["URGENTCAST_STORAGE"];
            // Connect to the storage account using the provided connection string
            CloudStorageAccount account = CloudStorageAccount.Parse(connString);
            // Create a client to deal with Blob Storage
            CloudBlobClient client = account.CreateCloudBlobClient();
            // Reference the container related to UrgentCast's storage
            _container = client.GetContainerReference(CONTAINER_NAME);
            // Create the container if it doesn't already exist
            if (_container.CreateIfNotExistsAsync().Result)
            {
                // Set the storage container read access as public
                var task = _container.SetPermissionsAsync(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
                // Wait for the asynchronous task to complete
                task.Wait();
            }
        }

        public IEnumerable<IListBlobItem> ListEpisodes(int maxResults = 5000)
        {
            // Instantiate the container reference in case it's first-time access
            if (_container == null)
            {
                Connect();
            }

            IEnumerable<IListBlobItem> episodes;
            try
            {
                // TODO : Figure out type casting to CloudBlockBlob
                episodes = ListBlobsSegmentedInFlatListingAsync(maxResults).Result.ToList();
                // episodes = episodes.Where(m => m.Name.Split('.')[1].Equals("mp3")).ToList();
            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return episodes;
        }

        private async Task<IEnumerable<IListBlobItem>> ListBlobsSegmentedInFlatListingAsync(int maxResults = 5000)
        {
            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;
            IEnumerable<IListBlobItem> result = null;

            // Call ListBlobsSegmentedAsync and enumerate the result segment returned, while the continuation token is non-null.
            // When the continuation token is null, the last page has been returned and execution can exit the loop.
            do
            {
                // This overload allows control of the page size. You can return all remaning results by passing null for the maxResults parameter,
                // or by calling a different overload.
                resultSegment = await _container.ListBlobsSegmentedAsync(EPISODES_FOLDER,
                    true, BlobListingDetails.All, maxResults, continuationToken, null, null);

                if (resultSegment.Results.Count<IListBlobItem>() > 0)
                {
                    result = resultSegment.Results;
                }

                // Get the continuation token
                continuationToken = resultSegment.ContinuationToken;
            }
            while (continuationToken != null);

            return result;
        }
    }
}
