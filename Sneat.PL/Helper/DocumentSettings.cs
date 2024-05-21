namespace Sneat.PL.Helper
{
    public static class  DocumentSettings
    {
        // Upload Files (Images and Files ==> Pdf)
        public static string UploadFile(IFormFile _File,string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\File",FolderName);
            string FileName = $"{Guid.NewGuid()}{_File.FileName}";
            string FilePath = Path.Combine(FolderPath, FileName);
            var Fs = new FileStream(FilePath, FileMode.Create);
            _File.CopyTo(Fs);
            return FileName;
        }

        // Delete Files
        public static void DeleteFile(string FileName , string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\File", FolderName);
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);

            }
        }
    }
}
