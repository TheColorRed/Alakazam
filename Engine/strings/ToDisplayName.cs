using System.Text.RegularExpressions;

namespace Alakazam.Engine {
  public static partial class Strings {
    public static string ToDisplayName(this string value) {
      return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
    }
  }
}