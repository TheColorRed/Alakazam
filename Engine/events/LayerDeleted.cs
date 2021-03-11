using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerDeleted;

    public static void OnLayerDeleted(object sender) {
      LayerDeleted?.Invoke(sender, EventArgs.Empty);
    }
  }
}