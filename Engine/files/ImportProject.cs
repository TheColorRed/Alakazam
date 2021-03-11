using System;
using System.IO;
using Alakazam.Engine.Settings;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;

namespace Alakazam.Engine.Import {
  public class ImportProject {

    public void Import(string importFile) {
      var id = Guid.NewGuid().ToString();
      var root = Path.Combine(Paths.ProjectsRoot(), id);

      ZipFile file = null;
      try {
        FileStream fs = File.OpenRead(importFile);
        file = new ZipFile(fs);

        foreach (ZipEntry zipEntry in file) {
          if (!zipEntry.IsFile) continue;

          var entryFileName = zipEntry.Name;
          var buffer = new byte[4096];
          var zipStream = file.GetInputStream(zipEntry);

          // Manipulate the output filename here as desired.
          var fullZipToPath = Path.Combine(root, entryFileName);
          var directoryName = Path.GetDirectoryName(fullZipToPath);

          if (directoryName.Length > 0) {
            Directory.CreateDirectory(directoryName);
          }

          var streamWriter = File.Create(fullZipToPath);
          StreamUtils.Copy(zipStream, streamWriter, buffer);
          streamWriter.Close();
        }
      } finally {
        if (file != null) {
          file.IsStreamOwner = true; // Makes close also shut the underlying stream
          file.Close(); // Ensure we release resources
        }
      }

      // Update the guid in the project settings
      var projectFile = Paths.ProjectFile(id);
      var json = File.ReadAllText(projectFile);
      dynamic obj = JsonConvert.DeserializeObject<object>(json);
      obj.guid = id;
      new ProjectSettings().Save(id, obj);
    }
  }
}