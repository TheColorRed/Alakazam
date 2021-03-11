using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerActionAdded;

    public static void OnLayerActionAdded(object sender) {
      LayerActionAdded?.Invoke(sender, EventArgs.Empty);
    }
  }
}