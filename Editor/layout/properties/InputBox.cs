using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Alakazam.Engine;
using Alakazam.Plugin;
using Convert = System.Convert;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static void InputBox(SerializedProperty property) {
      var action = (Action)property.Object;
      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
        // Margin = new Thickness(5, 5, 5, 0)
        Margin = actionPropertyIndex == 0 ? new Thickness(5, 0, 5, 0) : new Thickness(5, 5, 5, 0)
      };
      actionPropertyIndex++;

      var data = new BindingData();

      var label = property.Property.GetCustomAttribute<LabelAttribute>()?.text ?? property.Property.Name.ToDisplayName();

      var textBlock = new TextBlock {
        Text = label,
        Width = 100,
        MinWidth = 50,
        VerticalAlignment = VerticalAlignment.Center,
        Cursor = Cursors.SizeWE,
        DataContext = data
      };
      var textBox = new TextBox {
        Text = property.Property.GetValue(action).ToString(),
        Margin = new Thickness(5, 0, 0, 0),
        DataContext = data,
        MinWidth = 50
      };

      data.Action = action;
      data.Property = property.Property;
      data.Label = textBlock;
      data.Controls.Add(textBox);

      textBlock.MouseDown += OnSetDrag;

      textBox.GotFocus += OnInputGotFocus;
      textBox.LostFocus += OnInputLostFocus;
      textBox.KeyUp += OnInputChanged;
      textBox.PreviewKeyDown += OnPreviewKeyDown;

      DockPanel.SetDock(textBlock, Dock.Left);
      DockPanel.SetDock(textBox, Dock.Right);

      var range = property.Property.GetCustomAttribute<RangeAttribute>();
      var slider = new Slider();
      if (range != null) {
        data.Controls.Add(slider);
        if (property.Property.GetValue(action).GetType() == typeof(int))
          slider.Value = Convert.ToDouble(property.Property.GetValue(action));
        else
          slider.Value = (double)property.Property.GetValue(action);
        slider.Minimum = range.min;
        slider.Maximum = range.max;
        slider.TickFrequency = 1;
        slider.DataContext = data;
        slider.IsTabStop = false;
        // if (property.Property.GetValue(action).GetType() == typeof(int))
        slider.TickFrequency = 1;
        // else
        //   slider.TickFrequency = 0.01;
        // slider.LargeChange = 1;
        slider.IsSnapToTickEnabled = true;
        // slider.LargeChange = 5;
        // var collection = new DoubleCollection { range.initial };
        // slider.Ticks = collection;
        // slider.TickPlacement = TickPlacement.BottomRight;
        slider.ValueChanged += OnUpdateSliderValue;
      }
      var dockPanel = new DockPanel {
        Margin = new Thickness(0, 0, 0, 0)
      };


      dockPanel.Children.Add(textBox);
      if (range != null) {
        DockPanel.SetDock(slider, Dock.Left);
        dockPanel.Children.Add(slider);
      }

      panel.Children.Add(textBlock);
      panel.Children.Add(dockPanel);

      AddAction(panel, property);
    }
  }
}