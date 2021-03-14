using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using FontAwesome.Sharp;

namespace Alakazam.Editor {
  public static partial class GUILayout {

    public static Panel Header(string text) {
      var grid = new Grid {
        Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x50, 0x50, 0x50)),
        Height = 30,
        VerticalAlignment = VerticalAlignment.Center
      };

      // Define the Header
      var title = new TextBlock {
        Text = text,
        FontWeight = FontWeights.Bold,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(10, 0, 0, 0)
      };

      // Add the items to the grid
      grid.Children.Add(title);

      return grid;
    }
  }
}