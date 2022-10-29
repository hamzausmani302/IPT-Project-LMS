using System.IO;
namespace LMSApi2.Helpers
{
    public class FileUtils
    {
        public static bool isFileExist(string path) {

            return File.Exists(path);
                
            
        
        }
    }
}
