using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using Alakazam.Engine;
using Enum = System.Enum;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static void ComboBox(SerializedProperty property) {
      var action = (Action)property.Object;
      var propertyInfo = property.Property;
      var enumData = (Enum)property.Property.GetValue(action);

      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
        // Margin = new Thickness(5, 0, 5, 0)
        Margin = actionPropertyIndex == 0 ? new Thickness(5, 0, 5, 0) : new Thickness(5, 5, 5, 0)
      };
      actionPropertyIndex++;

      var options = (string[])Enum.GetNames(enumData.GetType());

      var label = propertyInfo.GetCustomAttribute<LabelAttribute>()?.text ?? propertyInfo.Name.ToDisplayName();

      // var actions = ActionsControl.control;
      var textBlock = new TextBlock {
        Text = label,
        Width = 100,
        VerticalAlignment = VerticalAlignment.Center
      };

      var comboBox = new ComboBox {
        Margin = new Thickness(5, 0, 2, 0),
        Foreground = Brushes.Black
      };

      comboBox.SelectionChanged += (sender, evt) => {
        var comboBox = (ComboBox)sender;
        var item = (ComboBoxItem)comboBox.SelectedItem;
        var context = (BindingData)item.DataContext;
        propertyInfo.SetValue(action, context.Value);
        EventBus.OnLayerActionChanged(sender);
      };

      comboBox.DropDownClosed += (sender, evt) => {
        // ActionsControl.OnActionsUpdated(sender);
      };

      foreach (var option in options) {
        var v = Enum.Parse(enumData.GetType(), option);
        var bindingData = new BindingData {
          Action = action,
          Property = propertyInfo,
          Label = textBlock,
          Input = comboBox,
          Value = v
        };
        var comboBoxItem = new ComboBoxItem {
          DataContext = bindingData,
          Content = new TextBlock { Text = option.ToDisplayName(), Foreground = Brushes.Black },
        };
        if (propertyInfo.GetValue(action).Equals(v)) {
          comboBox.SelectedItem = comboBoxItem;
        }
        comboBox.Items.Add(comboBoxItem);
      }

      DockPanel.SetDock(textBlock, Dock.Left);
      DockPanel.SetDock(comboBox, Dock.Right);

      panel.Children.Add(textBlock);
      panel.Children.Add(comboBox);

      AddAction(panel, property);
    }
  }
}