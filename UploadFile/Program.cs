namespace UploadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            UploadToDrive uploadToDrive = new UploadToDrive();
            //Enter the absolute path to the file that you wish to upload.
            uploadToDrive.UploadFilesToDrive(@"D:\Demo.docx");
        }
    }
}
