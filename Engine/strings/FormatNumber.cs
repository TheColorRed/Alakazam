
using System;
using System.Text.RegularExpressions;

namespace Alakazam.Engine {
  public static partial class Strings {
    public static double FormatNumberInput(string inputText) {
      var text = inputText.Trim().Length == 0 ? "0" : inputText;
      // replace anything that is no a number, dash, or period
      var replace = Regex.Replace(text, @"[^0-9-\.]", "");
      // Remove any dashes that are not at the beginning
      replace = Regex.Replace(replace, @"(\d)-", "$1");
      replace = Regex.Replace(replace, @"--*", "-");

      replace = replace.Trim().Length == 0 ? "0" : replace;
      replace = replace == "-" ? "0" : replace;

      return Math.Round(Convert.ToDouble(replace), 2, MidpointRounding.AwayFromZero);
    }
  }
}