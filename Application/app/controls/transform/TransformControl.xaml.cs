// using System;
// using System.ComponentModel;
// using System.Windows.Controls;
// using System.Windows.Input;
// using Alakazam.Events;

// namespace Alakazam.Controls {
//   public partial class TransformControl : UserControl, INotifyPropertyChanged {

//     public Layer SelectedLayer { get { return Engine.Engine.project.selectedLayer; } }

//     public int X {
//       get { return SelectedLayer.transform.Position.x; }
//       set {
//         SelectedLayer.transform.Position.x = value;
//         OnPropertyChanged("X");
//       }
//     }

//     public int Y {
//       get { return SelectedLayer.transform.Position.y; }
//       set {
//         SelectedLayer.transform.Position.y = value;
//         OnPropertyChanged("Y");
//       }
//     }

//     public int XScale {
//       get { return SelectedLayer.transform.Scale.x; }
//       set {
//         SelectedLayer.transform.Scale.x = value;
//         OnPropertyChanged("XScale");
//       }
//     }

//     public int YScale {
//       get { return SelectedLayer.transform.Scale.y; }
//       set {
//         SelectedLayer.transform.Scale.y = value;
//         OnPropertyChanged("YScale");
//       }
//     }

//     public double Rotation {
//       get { return SelectedLayer.transform.Rotation; }
//       set {
//         SelectedLayer.transform.Rotation = value;
//         OnPropertyChanged("Rotation");
//       }
//     }

//     public TransformControl() {
//       DataContext = this;
//       InitializeComponent();
//       EventBus.LayerSelectionChanged += OnLayerSelectionChanged;
//       EventBus.ProjectInitialized += OnLayerSelectionChanged;
//     }

//     public event PropertyChangedEventHandler PropertyChanged;
//     private void OnPropertyChanged(string propertyName) {
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//       var displayCanvas = (CanvasControl)App.Current.MainWindow.FindName("DisplayCanvas");
//       if (displayCanvas is CanvasControl) {
//         if (propertyName == "Rotation" || propertyName == "XScale" || propertyName == "YScale") {
//           // EventBus.OnLayerActionChanged(this);
//           // selectedLayer.ApplyActions();
//           SelectedLayer.ApplyActions();
//         }
//       }
//       EventBus.OnLayerTransformChanged(this);
//     }

//     protected void OnLayerSelectionChanged(object sender, EventArgs e) {
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rotation"));
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("XScale"));
//       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YScale"));

//     }

//     protected void OnFocus(object sender, EventArgs e) {
//       var input = (TextBox)sender;
//       input.Dispatcher.BeginInvoke(new Action(() => input.SelectAll()));
//     }

//     private void OnKeyUp(object sender, KeyEventArgs evt) {
//       var textBox = (TextBox)sender;

//       var amount = 1;
//       if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
//         amount = 10;
//       } else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) {
//         amount = 100;
//       } else if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt)) {
//         amount = 1000;
//       }

//       if (evt.Key == Key.Up) {
//         if (textBox.Name == "xInput") X += amount;
//         else if (textBox.Name == "yInput") Y += amount;
//         else if (textBox.Name == "sxInput") XScale += amount;
//         else if (textBox.Name == "syInput") YScale += amount;
//         else if (textBox.Name == "rotationInput") Rotation += amount;
//       } else if (evt.Key == Key.Down) {
//         if (textBox.Name == "xInput") X -= amount;
//         else if (textBox.Name == "yInput") Y -= amount;
//         else if (textBox.Name == "sxInput") XScale -= amount;
//         else if (textBox.Name == "syInput") YScale -= amount;
//         else if (textBox.Name == "rotationInput") Rotation -= amount;
//       }
//     }
//   }
// }
