using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerActionProcessChanged;

    public static void OnLayerActionProcessChanged(object sender) {
      LayerActionProcessChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}