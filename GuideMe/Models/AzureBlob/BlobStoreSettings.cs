using GuideMe.Interfaces.AzureBlob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.AzureBlob
{
    public class BlobStoreSettings : IBlobStoreSettings
    {
        public string ConnectionString { get; set; }
        public string ProfilePhotoContainer { get; set; }
        public string AudiofilesContainer { get; set; }
        public string LocationPhotosContainer { get; set; }
    }
}
