using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class TooltipAttribute : Attribute {

    public string toolTip;

    public TooltipAttribute(string tip) {
      toolTip = tip;
    }

  }
}