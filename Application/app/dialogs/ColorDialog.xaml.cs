using System.Windows;
using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.Dialogs {
  public partial class ColorDialog : Window {

    public MagickColor Color { get; set; } = MagickColors.Red;

    public ColorDialog() {
      DataContext = this;
      InitializeComponent();
    }

    public void OnAccept(object sender, RoutedEventArgs evt) {
      DialogResult = true;
    }
  }
}