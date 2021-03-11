using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler OpenColorPicker;

    public static void OnOpenColorPicker(object sender) {
      OpenColorPicker?.Invoke(sender, EventArgs.Empty);
    }
  }
}