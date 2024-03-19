namespace UploadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            UploadToDrive uploadToDrive = new UploadToDrive();
            uploadToDrive.UploadFilesToDrive(@"D:\Demo.docx");
        }
    }
}
