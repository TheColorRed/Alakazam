using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class LabelAttribute : Attribute {

    public string text;

    public LabelAttribute(string text) {
      this.text = text;
    }

  }
}