using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerTransformChanged;

    public static void OnLayerTransformChanged(object sender) {
      LayerTransformChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}