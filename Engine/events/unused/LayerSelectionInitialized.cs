// using System;

// namespace Alakazam.Events {
//   public partial class EventBus {
//     public static event EventHandler? LayerSelectionInitialized;

//     public static void OnLayerSelectionInitialized(object sender) {
//       if (EventBus.LayerSelectionInitialized != null) {
//         EventBus.LayerSelectionInitialized(sender, EventArgs.Empty);
//       }
//     }
//   }
// }