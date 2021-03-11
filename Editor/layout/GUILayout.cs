using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Controls;
using System.Windows.Input;
using Alakazam.Engine;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using EventArgs = System.EventArgs;
using SystemAction = System.Action;
using EventHandler = System.EventHandler;

namespace Alakazam.Editor {

  public class GUIAction {
    public readonly Action action;
    public readonly List<Panel> panels = new List<Panel>();
    private Panel header;
    private Layer layer;

    public Layer Layer {
      get => layer;
      set { if (layer == null) layer = value; }
    }

    public Panel Header {
      get => header;
      set { if (header == null) header = value; }
    }

    public GUIAction() { }

    public GUIAction(SerializedProperty property) {
      action = (Action)property.Object;
    }

    public GUIAction(Action action) {
      this.action = action;
    }
  }

  public static partial class GUILayout {
    public static event EventHandler ActionsUpdated;
    public static int actionPropertyIndex = 0;
    public static BindingData ActiveControl { get; set; }

    internal static List<GUIAction> actions = new List<GUIAction>();

    public static ImmutableList<GUIAction> Actions {
      get => ImmutableList.Create(actions.ToArray());
    }

    public static void OnActionsUpdated(object sender) {
      ActionsUpdated?.Invoke(sender, EventArgs.Empty);
    }

    public static void Reset() {
      actions.Clear();
    }

    private static void AddAction(Panel panel, Action action) {
      var guiAction = actions.Find(i => i.action == action);
      if (guiAction == null) {
        guiAction = new GUIAction(action);
        var enableCollapse = true;
        var enableDelete = true;
        var enableEnable = true;
        if (guiAction.action is Transform) {
          enableDelete = false;
          enableEnable = false;
        }
        guiAction.panels.Add(panel);
        guiAction.Header = GetActionHeader(guiAction, enableEnable, enableCollapse, enableDelete);
        actions.Add(guiAction);
        return;
      }
      guiAction.panels.Add(panel);
    }

    private static void AddAction(Panel panel, SerializedProperty property) {
      AddAction(panel, (Action)property.Object);
    }

    private static void AddPanel(Panel panel, Layer layer = null) {
      var guiAction = new GUIAction();
      guiAction.panels.Add(panel);
      guiAction.Layer = layer;
      actions.Add(guiAction);
    }

    public static void HandleNoDrawAction(Action action) {
      var guiAction = actions.Find(i => i.action == action);
      if (guiAction == null) {
        guiAction = new GUIAction(action);
        var enableCollapse = true;
        var enableDelete = true;
        var enableEnable = true;
        if (guiAction.action is Transform) {
          enableDelete = false;
          enableEnable = true;
        }
        guiAction.Header = GetActionHeader(guiAction, enableEnable, enableCollapse, enableDelete);
        actions.Add(guiAction);
        return;
      }
    }

    private static void OnPreviewKeyDown(object sender, KeyEventArgs e) {
      if (
        (e.Key >= Key.D0 && e.Key <= Key.D9) ||
        (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
        e.Key == Key.OemMinus || e.Key == Key.Subtract
        || e.Key == Key.OemPeriod || e.Key == Key.Decimal ||
        e.Key == Key.Tab || e.Key == Key.Delete || e.Key == Key.Back ||
         e.Key == Key.Left || e.Key == Key.Right ||
        (e.Key == Key.A && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
      ) {
        e.Handled = false;
      } else {
        e.Handled = true;
      }
    }

    private static void OnInputChanged(object sender, EventArgs evt) {
      var data = ActiveControl;
      if (data == null) return;
      if (ModifyValue.SetValue(data)) {
        EventBus.OnLayerActionChanged(sender);
      }
    }

    private static void OnSetDrag(object sender, EventArgs evt) {
      // ActionsControl.isDragValue = true;
      ActiveControl = (BindingData)((TextBlock)sender).DataContext;
    }

    private static void OnInputGotFocus(object sender, EventArgs evt) {
      var input = (TextBox)sender;
      ActiveControl = (BindingData)input.DataContext;
      input.Dispatcher.BeginInvoke(new SystemAction(() => input.SelectAll()));
    }

    private static void OnInputLostFocus(object sender, EventArgs evt) {
      // timer.Stop();
      // if (ActiveControl == null) return;
      // Debugger.Log(Keyboard.FocusedElement);
      // ModifyValue.SetValue(ActiveControl, (Control)sender);
      // EventBus.OnLayerActionChanged(sender);
    }

    private static void OnUpdateSliderValue(object sender, EventArgs evt) {
      var slider = (Slider)sender;
      ActiveControl = (BindingData)slider.DataContext;
      if (ModifyValue.SetValue(ActiveControl)) {
        EventBus.OnLayerActionChanged(sender);
      }
    }

    private static void OnToggleCheckBox(object sender, EventArgs evt) {
      var checkBox = (CheckBox)sender;
      ActiveControl = (BindingData)checkBox.DataContext;
      if (ActiveControl.Action is Action) {
        ActiveControl.Property.SetValue(ActiveControl.Action, checkBox.IsChecked);
      } else if (ActiveControl.Layer != null) {
        ActiveControl.Property.SetValue(ActiveControl.Layer, checkBox.IsChecked);
      }
      EventBus.OnLayerActionChanged(sender);
      OnActionsUpdated(sender);
    }
  }
}