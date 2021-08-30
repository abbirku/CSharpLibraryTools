using Google.Apis.Drive.v3.Data;
using System.Collections.Generic;

namespace CoreActivities.GoogleDriveApi.Models
{
    public class GoogleDriveFiles
    {
        public string NextPageToken { get; set; }
        public IList<File> Files { get; set; }
    }
}
