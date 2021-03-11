using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerFiltered;

    public static void OnLayerFiltered(object sender) {
      LayerFiltered?.Invoke(sender, EventArgs.Empty);
    }
  }
}