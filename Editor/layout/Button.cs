using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alakazam.Plugin;
using Color = System.Windows.Media.Color;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static Button Button(string text) {
      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
        Margin = actionPropertyIndex == 0 ? new Thickness(5, 0, 5, 0) : new Thickness(5, 5, 5, 0)
      };

      var button = new Button {
        Content = new TextBlock { Text = text },
        Padding = new Thickness(3),
        Background = new SolidColorBrush(Color.FromRgb(64, 71, 84))
      };

      panel.Children.Add(button);

      AddPanel(panel);

      return button;
    }
  }
}