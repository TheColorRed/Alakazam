using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Alakazam.Engine.Export {
  public class ExportProject {

    private readonly Project project;

    public ExportProject(Project project) {
      this.project = project;
    }

    public void Export(string outfile) {
      var root = Paths.ProjectRootPath(project);
      var resources = Path.Combine(root, "resources");

      var filenames = new List<string>();
      DirSearch(resources, ref filenames);
      filenames.Add(Paths.ProjectFile(project));

      project.Save();

      var outputStream = new ZipOutputStream(File.Create(outfile));
      var buffer = new byte[4096];
      foreach (var file in filenames) {

        var path = file.Replace(root, "");
        var entry = new ZipEntry(path);
        outputStream.PutNextEntry(entry);
        var fs = File.OpenRead(file);
        // Using a fixed size buffer here makes no noticeable difference for output
        // but keeps a lid on memory usage.
        int sourceBytes;

        do {
          sourceBytes = fs.Read(buffer, 0, buffer.Length);
          outputStream.Write(buffer, 0, sourceBytes);
        } while (sourceBytes > 0);
      }
      outputStream.Finish();
      outputStream.Close();
    }


    private void DirSearch(string sDir, ref List<string> files) {
      foreach (string d in Directory.GetDirectories(sDir)) {
        foreach (string f in Directory.GetFiles(d)) {
          files.Add(f);
        }
        DirSearch(d, ref files);
      }
    }
  }
}