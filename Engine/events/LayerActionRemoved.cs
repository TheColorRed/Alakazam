using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerActionRemoved;

    public static void OnLayerActionRemoved(object sender) {
      LayerActionRemoved?.Invoke(sender, EventArgs.Empty);
    }
  }
}