using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class RangeAttribute : Attribute {

    public double min;
    public double max;
    public double initial;

    public RangeAttribute(double min, double max, double initial = 0.0) {
      this.min = min;
      this.max = max;
      this.initial = Math.Clamp(initial, min, max);
    }

  }
}