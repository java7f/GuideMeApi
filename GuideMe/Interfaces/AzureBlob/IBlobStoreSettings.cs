using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Interfaces.AzureBlob
{
    public interface IBlobStoreSettings
    {
        string ConnectionString { get; set; }
        string ProfilePhotoContainer { get; set; }
        string AudiofilesContainer { get; set; }
        string LocationPhotosContainer { get; set; }
    }
}
