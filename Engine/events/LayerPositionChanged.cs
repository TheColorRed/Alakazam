using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerPositionChanged;

    public static void OnLayerPositionChanged(object sender) {
      LayerPositionChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}