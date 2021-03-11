using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerBlendChanged;

    public static void OnLayerBlendChanged(object sender) {
      LayerBlendChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}