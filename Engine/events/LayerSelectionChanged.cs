using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerSelectionChanged;

    public static void OnLayerSelectionChanged(object sender) {
      LayerSelectionChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}