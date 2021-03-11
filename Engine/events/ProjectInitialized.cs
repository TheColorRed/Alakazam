using System;

namespace Alakazam.Engine.Events {
  public partial class EventBus {
    public static event EventHandler ProjectInitialized;

    public static void OnProjectInitialized(object sender) {
      ProjectInitialized?.Invoke(sender, EventArgs.Empty);
    }
  }
}