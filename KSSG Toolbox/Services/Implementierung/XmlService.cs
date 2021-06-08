using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services.Impl
{
  internal class XmlService : IXmlService
  {
    public void MetadataItemToXml(IMetadataItem metadataItem, string targetDir) { 
    var xmlSerializer = new XmlSerializer(typeof(MetadataItem));

    var streamWriter = new StreamWriter(Path.Combine(targetDir, metadataItem.MetadataFilename));
    xmlSerializer.Serialize(streamWriter, metadataItem);
    streamWriter.Flush();
    streamWriter.Close();

    File.Copy(metadataItem.OrginalPath, Path.Combine(targetDir, metadataItem.ContentFilename));
    
  }

    public IList<IMetadataItem> XmlToMetadataItems(IList<string> metadataFile)
    {
      IList<IMetadataItem> metadataItemList = new List<IMetadataItem>();
      foreach (var m in metadataFile)
      {
        var xmlSerializer = new XmlSerializer(typeof(MetadataItem));
        var streamReader = new StreamReader(m);
        var metadata = (MetadataItem)xmlSerializer.Deserialize(streamReader);
        metadataItemList.Add(metadata);
      }

      return metadataItemList;
    }
  }
}
