using System;

namespace Alakazam.Editor {
  [AttributeUsage(AttributeTargets.Property)]
  public class StepAttribute : Attribute {

    public double value;

    public StepAttribute(double amount) {
      value = amount;
    }

  }
}