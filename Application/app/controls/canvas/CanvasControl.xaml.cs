using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Alakazam.Commands;
using Alakazam.Engine;
using Alakazam.Engine.Events;
using Alakazam.ImageMagick;

namespace Alakazam.Controls {
  public partial class CanvasControl : UserControl, INotifyPropertyChanged {

    public readonly Project project = Engine.Engine.project;

    public event PropertyChangedEventHandler PropertyChanged;
    public BitmapImage displayImage;
    public BitmapImage DisplayImage {
      get { return displayImage; }
      set {
        if (displayImage != value) {
          displayImage = value;
          if (PropertyChanged != null) {
            MainWindow.OnPropertyChanged(PropertyChanged, this, "DisplayImage");
          }
        }
      }
    }

    public BitmapImage CheckerBoard {
      get { return project.checkerBoardSmall.ToBitmapImage(); }
    }

    public int imageWidth = 0;
    public int ImageWidth {
      get { return imageWidth; }
      set {
        imageWidth = value;
        if (PropertyChanged != null) {
          MainWindow.OnPropertyChanged(PropertyChanged, this, "ImageWidth");
        }
      }
    }

    public int imageHeight = 0;
    public int ImageHeight {
      get { return imageHeight; }
      set {
        imageHeight = value;
        if (PropertyChanged != null) {
          MainWindow.OnPropertyChanged(PropertyChanged, this, "ImageHeight");
        }
      }
    }

    public CanvasControl() {
      DataContext = this;
      InitializeComponent();
      EventBus.LayerFiltered += OnCompose;
      EventBus.LayerBlendChanged += OnCompose;
      EventBus.LayerTransformChanged += OnCompose;
      EventBus.LayerVisibilityChanged += OnCompose;
      EventBus.LayerPositionChanged += OnCompose;
      EventBus.LayerDeleted += OnCompose;
    }

    private void OnCompose(object sender, EventArgs e) {
      Compose();
    }

    public void OnLoaded(object sender, EventArgs e) {
      ImageWidth = project.size.width;
      ImageHeight = project.size.height;
    }

    private void Compose() {
      Task.Run(() => {
        DisplayImage = project.Compose();
      });
    }
  }
}