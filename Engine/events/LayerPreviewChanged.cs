using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler LayerPreviewChanged;

    public static void OnLayerPreviewChanged(object sender) {
      LayerPreviewChanged?.Invoke(sender, EventArgs.Empty);
    }
  }
}