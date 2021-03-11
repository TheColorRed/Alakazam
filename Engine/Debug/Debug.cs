using System;
using System.Collections.Generic;

namespace Alakazam.Engine {
  public class Debug {
    private static readonly Dictionary<string, DateTime> times = new Dictionary<string, DateTime>();

    public static void Time(string tag) {
      if (times.ContainsKey(tag)) {
        times.Remove(tag);
      }
      times.Add(tag, DateTime.Now);
    }

    public static void TimeEnd(string tag) {
      times.TryGetValue(tag, out DateTime value);
      if (value != null) {
        var t = DateTime.Now - value;
        Console.WriteLine(tag + ": " + t.TotalMilliseconds.ToString());
      }
    }

    public static void Log(params object[] items) {
      foreach (var item in items) {
        Console.WriteLine(item);
      }
    }

    public static void Warn(params object[] items) {
      foreach (var item in items) {
        Console.WriteLine(item.ToString(), ConsoleColor.DarkYellow);
      }
      Console.ResetColor();
    }
  }
}