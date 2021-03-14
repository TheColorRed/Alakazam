using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using FontAwesome.Sharp;

namespace Alakazam.Editor {
  public static partial class GUILayout {

    // [JsonIgnore]
    // public Project Project { get { return Engine.Engine.project; } }

    internal static Panel GetActionHeader(GUIAction guiAction, bool enableable, bool toggleable, bool deleteable) {
      var action = guiAction.action;
      var grid = new Grid {
        Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x50, 0x50, 0x50)),
        Margin = new Thickness(0, 0, 0, action.Collapsed ? 0 : 5)
      };
      var c1 = new ColumnDefinition();
      var c2 = new ColumnDefinition { Width = GridLength.Auto };
      grid.ColumnDefinitions.Add(c1);
      grid.ColumnDefinitions.Add(c2);

      var stackPanel = new StackPanel {
        Orientation = Orientation.Horizontal
      };

      // Define the icons
      var openIcon = new IconBlock {
        Icon = IconChar.CaretDown, FontSize = 12, Width = 12
      };
      var collapseIcon = new IconBlock {
        Icon = IconChar.CaretRight, FontSize = 12, Width = 12
      };
      var checkedIcon = new IconBlock {
        Icon = IconChar.CheckSquare, FontSize = 12, Width = 12
      };
      var uncheckedIcon = new IconBlock {
        Icon = IconChar.Square, FontSize = 12, Width = 12
      };
      var deleteIcon = new IconBlock {
        Icon = IconChar.TrashAlt, FontSize = 12, Width = 12
      };

      // Define the buttons
      var expandCollapseToggle = new Button {
        Content = action.Collapsed ? collapseIcon : openIcon,
        Margin = new Thickness(2, 0, 2, 0),
        Background = Brushes.Transparent,
        IsTabStop = false
      };
      var enableDisableToggle = new Button {
        Content = action.Enabled ? checkedIcon : uncheckedIcon,
        Margin = new Thickness(2, 0, 2, 0),
        Background = Brushes.Transparent,
        IsTabStop = false
      };
      var deleteAction = new Button {
        Content = deleteIcon,
        Margin = new Thickness(2, 0, 5, 0),
        Background = Brushes.Transparent,
        HorizontalAlignment = HorizontalAlignment.Right,
        IsTabStop = false
      };

      // Define the button events
      expandCollapseToggle.Click += (sender, e) => {
        action.Collapsed = !action.Collapsed;
        OnActionsUpdated(sender);
      };
      enableDisableToggle.Click += (sender, e) => {
        action.Enabled = !action.Enabled;
        guiAction.Layer.ApplyActions();
        OnActionsUpdated(sender);
        EventBus.OnLayerActionChanged(sender);
      };
      deleteAction.Click += (sender, e) => {
        guiAction.Layer.actions.Remove(action);
        guiAction.Layer.ApplyActions();
        OnActionsUpdated(sender);
        EventBus.OnLayerActionRemoved(sender);
      };

      // Define the Header
      var title = new TextBlock {
        Text = action.Name,
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(5)
      };

      // Add the items to the stack panel
      if (guiAction.panels.Count > 0 && toggleable)
        stackPanel.Children.Add(expandCollapseToggle);
      else
        enableDisableToggle.Margin = new Thickness(20, 0, 2, 0);

      if (enableable) stackPanel.Children.Add(enableDisableToggle);
      stackPanel.Children.Add(title);

      // Define the grid columns
      Grid.SetColumn(stackPanel, 0);
      Grid.SetColumn(deleteAction, 1);

      // Add the items to the grid
      grid.Children.Add(stackPanel);
      if (deleteable) grid.Children.Add(deleteAction);

      return grid;
    }
  }
}