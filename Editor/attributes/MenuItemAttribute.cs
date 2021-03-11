using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Class)]
  public class MenuItemAttribute : Attribute {

    public string path;
    public int[] order;

    public MenuItemAttribute(string path, params int[] order) {
      this.path = path;
      this.order = order;
    }

  }
}