using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Class)]
  public class MenuItemSeparatorAttribute : Attribute {
    /// <summary>
    /// Adds a separator above the current item.
    /// </summary>
    public MenuItemSeparatorAttribute() { }
  }
}