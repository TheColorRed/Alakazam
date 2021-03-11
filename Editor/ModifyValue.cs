using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Alakazam.Engine;

namespace Alakazam.Editor {

  enum Direction { Add, Subtract }

  public class ModifyValue {
    public static bool SetValue(BindingData data, Control control = null) {
      var number = 0.0;
      var element = control ?? Keyboard.FocusedElement;
      if (element is Slider slider) number = slider.Value;
      else if (element is TextBox textBox) {
        if (
          textBox.Text == "" ||
          textBox.Text.EndsWith("-") ||
          textBox.Text.EndsWith(".")
        ) return false;
        number = Strings.FormatNumberInput(textBox.Text);
      }

      var property = data.Property;
      number = ApplyNumberFormatting(number, property);

      if (data.Value is Vector vector && data.Field is FieldInfo field) {
        if (field.Name == "x") vector.x = Convert.ToInt32(number);
        if (field.Name == "y") vector.y = Convert.ToInt32(number);
      } else {
        var type = property.GetValue(data.Action).GetType();
        var val = Convert.ChangeType(number.ToString(), type);
        property.SetValue(data.Action, val);
      }
      SyncControls(data, number);
      return true;
    }

    public static void SyncControls(BindingData data, double value) {
      var formatted = Regex.Replace(value.ToString("F"), @"\.00$", "");
      foreach (var control in data.Controls) {
        if (control is TextBox input) {
          input.Text = formatted;
        } else if (control is Slider slider) {
          slider.Value = value;
        }
      }
    }

    public static void IncrementValue(BindingData data) {
      ApplyValueChange(data, Direction.Add);
    }

    public static void DecrementValue(BindingData data) {
      ApplyValueChange(data, Direction.Subtract);
    }

    private static void ApplyValueChange(BindingData data, Direction dir) {
      object val;
      var property = data.Property;
      if (data.Value is Vector vector && data.Field is FieldInfo field) {
        if (dir == Direction.Add) {
          vector.x = field.Name == "x" ? vector.x + 1 : vector.x;
          vector.y = field.Name == "y" ? vector.y + 1 : vector.y;
        } else if (dir == Direction.Subtract) {
          vector.x = field.Name == "x" ? vector.x - 1 : vector.x;
          vector.y = field.Name == "y" ? vector.y - 1 : vector.y;
        }
        val = vector;
      } else {
        var type = property.GetValue(data.Action).GetType();
        var number = Strings.FormatNumberInput(((TextBox)data.Input).Text);
        var range = property.GetCustomAttribute<RangeAttribute>();
        var min = property.GetCustomAttribute<MinAttribute>();
        var step = property.GetCustomAttribute<StepAttribute>();
        if (range != null) {
          if (dir == Direction.Add)
            number = Math.Clamp(number + (step?.value ?? 1), range.min, range.max);
          else if (dir == Direction.Subtract)
            number = Math.Clamp(number - (step?.value ?? 1), range.min, range.max);
        } else if (min != null) {
          if (dir == Direction.Add)
            number = Math.Clamp(number + (step?.value ?? 1), min.value, double.MaxValue);
          else if (dir == Direction.Subtract)
            number = Math.Clamp(number - (step?.value ?? 1), min.value, double.MaxValue);
        } else {
          if (dir == Direction.Add)
            number += step?.value ?? 1;
          else if (dir == Direction.Subtract)
            number -= step?.value ?? 1;
        }
        val = Convert.ChangeType(number.ToString(), type);
        property.SetValue(data.Action, val);
      }
      if (data.Input is TextBox input) {
        var inputText = val;
        if (val is Vector vector1) {
          inputText = (data.Field.Name == "x" ? vector1.x : vector1.y).ToString();
        }
        input.Text = inputText.ToString();
      }
    }

    public static double ApplyNumberFormatting(double number, PropertyInfo property) {
      var range = property.GetCustomAttribute<RangeAttribute>();
      var min = property.GetCustomAttribute<MinAttribute>();
      var max = property.GetCustomAttribute<MaxAttribute>();
      if (range != null) {
        return Math.Clamp(number, range.min, range.max);
      } else if (min != null) {
        return Math.Clamp(number, min.value, double.MaxValue);
      } else if (max != null) {
        return Math.Clamp(number, double.MinValue, max.value);
      }
      return number;
    }
  }
}