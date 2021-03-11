using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class MaxAttribute : Attribute {

    public double value;

    public MaxAttribute(double max) {
      value = max;
    }

  }
}