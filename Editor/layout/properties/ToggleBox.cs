using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Alakazam.Engine;
using Alakazam.Plugin;
using Convert = System.Convert;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static void ToggleBox(SerializedProperty property) {
      var action = property.Object;
      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
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
        DataContext = data
      };

      var checkBox = new CheckBox {
        Margin = new Thickness(5, 0, 0, 0),
        IsChecked = (bool)property.Property.GetValue(action),
        DataContext = data
      };

      if (action is Action action1) data.Action = action1;
      else if (property.Object is Layer layer) data.Layer = layer;
      data.Property = property.Property;
      data.Label = textBlock;
      data.Value = (bool)property.Value;
      data.Controls.Add(checkBox);

      checkBox.Click += OnToggleCheckBox;

      DockPanel.SetDock(textBlock, Dock.Left);
      DockPanel.SetDock(checkBox, Dock.Right);

      panel.Children.Add(textBlock);
      panel.Children.Add(checkBox);

      if (action is Action)
        AddAction(panel, property);
      else if (property.Object is Layer layer)
        AddPanel(panel, layer);
    }
  }
}