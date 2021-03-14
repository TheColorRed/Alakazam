using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerMaskChanged;

    public static void OnLayerMaskChanged(object sender) {
      LayerMaskChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}