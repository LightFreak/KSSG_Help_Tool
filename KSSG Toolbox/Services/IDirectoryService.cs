using System.IO;
using KSSG.Toolbox.Model;

namespace KSSG.Toolbox.Services
{
    public interface IDirectoryService
    {
        string Combine(string targetPath, string valutaYear);

        string GetExtension(string sourcePath);

        string Combine(string targetPath, string valutaYear, string valutaMonth);
        
        void CreateDirectoryFolder(string targetDir);

        string[] GetFiles(string targetPath, string pathPattern);

        DirectoryInfo[] GetSubFolder(string targetPath);

        void DeleteFile(IMetadataItem metadataItem, bool deleteFile);
        
    }
}