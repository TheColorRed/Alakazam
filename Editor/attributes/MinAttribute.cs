using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class MinAttribute : Attribute {

    public double value;

    public MinAttribute(double min) {
      value = min;
    }

  }
}