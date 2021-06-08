using System.Collections.Generic;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Properties;

namespace ZbW.Testing.Dms.Client.Services.Impl
{
  public class FileSystemService
  {
    private string TargetPath = Settings.Default.DefaultPath;

    public FileSystemService()
    {
      XmlService = new XmlService();
      FilenameGeneratorService = new FilenameGeneratorServiceService();
      DirectoryService = new DirectoryService();
      GuidGeneratorService = new GuidGeneratorService();
    }

    public FileSystemService(IXmlService xmlService, IFilenameGeneratorService filenameGeneratorService, IDirectoryService directoryService, IGuidGeneratorService guidGeneratorService)
    {
      XmlService = xmlService;
      FilenameGeneratorService = filenameGeneratorService;
      DirectoryService = directoryService;
      GuidGeneratorService = guidGeneratorService;
    }

    private IXmlService XmlService { get; }
    private IFilenameGeneratorService FilenameGeneratorService { get; }
    private IDirectoryService DirectoryService { get; }
    private IMetadataItem MetaDataIteam { get; set; }
    private IGuidGeneratorService GuidGeneratorService { get;  }

    public void AddFile(IMetadataItem metadataItem, bool isRemoveFileEnabled, string sourcePath)
    {
      MetaDataIteam = metadataItem;

      var documentId = GuidGeneratorService.GetNewGuid();
      var extension = DirectoryService.GetExtension(sourcePath);
      MetaDataIteam.ContentFilename = FilenameGeneratorService.GetContentFilename(documentId, extension);
      MetaDataIteam.MetadataFilename = FilenameGeneratorService.GetMetadataFilename(documentId);

      var targetDir = DirectoryService.Combine(TargetPath, MetaDataIteam.ValutaYear, MetaDataIteam.ValutaMonth);

      MetaDataIteam.OrginalPath = sourcePath;
      MetaDataIteam.PathInRepo = targetDir + @"\" + MetaDataIteam.ContentFilename;
      MetaDataIteam.ContentFileExtension = extension;
      MetaDataIteam.ContentFilename = MetaDataIteam.ContentFilename;
      MetaDataIteam.DocumentId = documentId;

      DirectoryService.CreateDirectoryFolder(targetDir);

      XmlService.MetadataItemToXml(MetaDataIteam, targetDir);
      DirectoryService.DeleteFile(MetaDataIteam, isRemoveFileEnabled);
    }

    public IList<IMetadataItem> LoadMetadata()
    {
      var metadataFile = GetAllFiles();
      var metadataList = XmlService.XmlToMetadataItems(metadataFile);
      return metadataList;
    }

    private IList<string> GetAllFiles()
    {
      var metadataFile = new List<string>();
      var nameOfAllSubFolder = GetAllSubFolder(TargetPath);
      foreach (var d in nameOfAllSubFolder)
      {
     
        var list = DirectoryService.GetFiles(TargetPath + @"\" + d, @"*_Metadata.xml");
        metadataFile.AddRange(list);
      }

      return metadataFile;
    }

    private IList<string> GetAllSubFolder(string Path)
    {
      var nameOfAllSubFolder = new List<string>();
      var subFolder = DirectoryService.GetSubFolder(Path);
      foreach (var n in subFolder)
      {
          var Sub = DirectoryService.Combine(Path, n.Name);
          var subFolderList = (List<string>) GetAllSubFolder(Sub);
          foreach (var folder in subFolderList)
          {
              nameOfAllSubFolder.Add(DirectoryService.Combine(n.Name, folder));
          }
          nameOfAllSubFolder.Add(n.Name);
      }

     
      return nameOfAllSubFolder;
    }

  

    
  }
}