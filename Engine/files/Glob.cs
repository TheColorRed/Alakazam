using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoGlob = GlobExpressions.Glob;

namespace Alakazam.Engine {
  public class Glob {
    public static string[] Find(string root, string pattern) {
      return DoGlob.Files(root, pattern)
        .Select(i => Path.Combine(root, i))
        .ToArray();
    }
  }
}