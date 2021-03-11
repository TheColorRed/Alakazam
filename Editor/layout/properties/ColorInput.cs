using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alakazam.Engine;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using ImageMagick;
using Color = System.Windows.Media.Color;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static void ColorInput(SerializedProperty property) {
      var action = (Action)property.Object;
      var propertyInfo = property.Property;
      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
        Margin = actionPropertyIndex == 0 ? new Thickness(5, 0, 5, 0) : new Thickness(5, 5, 5, 0)
      };
      actionPropertyIndex++;

      var label = propertyInfo.GetCustomAttribute<LabelAttribute>()?.text ?? propertyInfo.Name.ToDisplayName();

      var textBlock = new TextBlock {
        Text = label,
        Width = 100,
        VerticalAlignment = VerticalAlignment.Center
      };

      var color = ((MagickColor)property.Property.GetValue(action)).ToByteArray();

      var colorButton = new Button {
        Margin = new Thickness(5, 0, 0, 0),
        Background = new SolidColorBrush(Color.FromArgb(color[3], color[0], color[1], color[2]))
      };

      colorButton.Click += (sender, evt) => {
        if (sender is Button button) {
          button.DataContext = property.Property.GetValue(action);
          EventBus.OnOpenColorPicker(sender);
        }
      };

      DockPanel.SetDock(textBlock, Dock.Left);
      DockPanel.SetDock(colorButton, Dock.Right);

      panel.Children.Add(textBlock);
      panel.Children.Add(colorButton);

      AddAction(panel, property);
    }
  }
}