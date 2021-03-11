// using System;

// namespace Alakazam.Events {
//   public partial class EventBus {
//     public static event EventHandler? ProjectUpdated;

//     public static void OnProjectUpdated(object sender) {
//       if (EventBus.ProjectUpdated != null) {
//         EventBus.ProjectUpdated(sender, EventArgs.Empty);
//       }
//     }
//   }
// }