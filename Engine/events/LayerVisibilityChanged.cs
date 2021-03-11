using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerVisibilityChanged;

    public static void OnLayerVisibilityChanged(object sender) {
      LayerVisibilityChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}