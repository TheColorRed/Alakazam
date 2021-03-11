using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerActionChanged;

    public static void OnLayerActionChanged(object sender) {
      LayerActionChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}