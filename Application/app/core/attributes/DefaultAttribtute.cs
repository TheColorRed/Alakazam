using System;

namespace Alakazam {
  [AttributeUsage(AttributeTargets.Property)]
  public class DefaultAttribute : Attribute {

    public double defaultValue;

    public DefaultAttribute(double defaultValue) {
      this.defaultValue = defaultValue;
    }

  }
}