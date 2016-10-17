using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrgentCast.Services
{
    public interface IStorageService
    {
        IEnumerable<IListBlobItem> ListEpisodes(int maxResults = 5000);
    }
}
