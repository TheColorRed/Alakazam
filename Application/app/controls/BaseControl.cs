using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Alakazam.Controls {
  public abstract class BaseControl : UserControl, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}