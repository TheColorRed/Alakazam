using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Alakazam.Engine;
using Alakazam.Plugin;
using Vector = Alakazam.Engine.Vector;

namespace Alakazam.Editor {
  public static partial class GUILayout {
    public static void VectorInput(SerializedProperty property) {
      var propertyInfo = property.Property;
      var action = (Action)property.Object;
      var panel = new DockPanel {
        VerticalAlignment = VerticalAlignment.Center,
        Margin = actionPropertyIndex == 0 ? new Thickness(5, 0, 5, 0) : new Thickness(5, 5, 5, 0)
      };
      actionPropertyIndex++;

      var textBlock = new TextBlock {
        Text = propertyInfo.Name.ToDisplayName(),
        Width = 100,
        VerticalAlignment = VerticalAlignment.Center
      };

      var tooltip = property.Property.GetCustomAttribute<TooltipAttribute>();
      if (tooltip is TooltipAttribute attr) {
        textBlock.ToolTip = attr.toolTip;
        ToolTipService.SetShowDuration(textBlock, int.MaxValue);
      }

      var bindingDataX = new BindingData();
      var bindingDataY = new BindingData();

      var textBlockX = new TextBlock {
        Text = "X",
        VerticalAlignment = VerticalAlignment.Center,
        Cursor = Cursors.SizeWE,
        Margin = new Thickness(0, 0, 5, 0),
        DataContext = bindingDataX
      };
      var textBlockY = new TextBlock {
        Text = "Y",
        VerticalAlignment = VerticalAlignment.Center,
        Cursor = Cursors.SizeWE,
        Margin = new Thickness(5, 0, 5, 0),
        DataContext = bindingDataY
      };

      var textBoxX = new TextBox {
        Text = ((Vector)propertyInfo.GetValue(action)).x.ToString(),
        DataContext = bindingDataX
      };
      var textBoxY = new TextBox {
        Text = ((Vector)propertyInfo.GetValue(action)).y.ToString(),
        DataContext = bindingDataY
      };

      bindingDataX.Action = action;
      bindingDataX.Property = propertyInfo;
      bindingDataX.Field = propertyInfo.GetValue(action).GetType().GetField("x");
      bindingDataX.Input = textBoxX;
      bindingDataX.Label = textBlockX;
      bindingDataX.Value = propertyInfo.GetValue(action);

      bindingDataY.Action = action;
      bindingDataY.Property = propertyInfo;
      bindingDataY.Field = propertyInfo.GetValue(action).GetType().GetField("y");
      bindingDataY.Input = textBoxY;
      bindingDataY.Label = textBlockY;
      bindingDataY.Value = propertyInfo.GetValue(action);

      textBlockX.MouseDown += OnSetDrag;

      textBoxX.GotFocus += OnInputGotFocus;
      textBoxX.LostFocus += OnInputLostFocus;
      textBoxX.PreviewKeyDown += OnPreviewKeyDown;
      textBoxX.KeyUp += OnInputChanged;

      textBlockY.MouseDown += OnSetDrag;
      textBoxY.GotFocus += OnInputGotFocus;
      textBoxY.LostFocus += OnInputLostFocus;
      textBoxY.PreviewKeyDown += OnPreviewKeyDown;
      textBoxY.KeyUp += OnInputChanged;

      var inputBlock = new UniformGrid {
        Rows = 1,
        Margin = new Thickness(5, 0, 0, 0)
      };

      var panelX = new DockPanel();
      var panelY = new DockPanel();

      panelX.Children.Add(textBlockX);
      panelX.Children.Add(textBoxX);
      panelY.Children.Add(textBlockY);
      panelY.Children.Add(textBoxY);

      inputBlock.Children.Add(panelX);
      inputBlock.Children.Add(panelY);

      textBoxX.PreviewKeyDown += OnPreviewKeyDown;
      textBoxY.PreviewKeyDown += OnPreviewKeyDown;

      DockPanel.SetDock(textBlock, Dock.Left);
      DockPanel.SetDock(inputBlock, Dock.Right);

      DockPanel.SetDock(textBlockX, Dock.Left);
      DockPanel.SetDock(textBoxX, Dock.Right);

      DockPanel.SetDock(textBlockY, Dock.Left);
      DockPanel.SetDock(textBoxY, Dock.Right);

      panel.Children.Add(textBlock);
      panel.Children.Add(inputBlock);

      AddAction(panel, property);
    }
  }
}