using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFile
{
    public class UploadToDrive
    {
        public void UploadFilesToDrive(string filePath)
        {
            try
            {
                string credentialsPath = "Credentials.json";
                string folderId = "enter_your_folderId";
                string fileToUpload = filePath;

                GoogleCredential credential;
                using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
                    {
                        DriveService.ScopeConstants.DriveFile
                    });
                    var service = new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "File Upload Console App"
                    });

                    var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = Path.GetFileName(fileToUpload),
                        Parents = new List<string> { folderId }
                    };

                    FilesResource.CreateMediaUpload request;
                    using (var streamFile = new FileStream(fileToUpload, FileMode.Open))
                    {
                        request = service.Files.Create(fileMetaData, streamFile, "");
                        request.Fields = "id";
                        request.Upload();
                    }
                    var uploadedFile = request.ResponseBody;
                    Console.WriteLine($"File '{fileMetaData.Name}' uploaded with ID: {uploadedFile.Id}");
                }
            }
            catch (Google.GoogleApiException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                if (ex.Error != null)
                {
                    Console.WriteLine($"HTTP Status Code: {ex.Error.Code}");
                    Console.WriteLine($"Error Message: {ex.Error.Message}");
                }
            }
            
        }
    }
}
