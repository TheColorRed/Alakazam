using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using ImageMagick;
using Alakazam.Engine.Settings;
using Alakazam.Engine;
using Alakazam.Engine.Events;
using Paths = Alakazam.Engine.Paths;

namespace Alakazam {
  public partial class MainWindow : Window {

    // public static Project project =
    // new ProjectSettings()
    // .Load("47e64963-c59b-48a6-a6be-295a7c188881");
    // .Load("95d1jg74-761d-460d-889d-003c8236nd9g");

    public Project Project => Engine.Engine.project;

    public static ApplicationSettings settings;

    public int LayersGridSplitterWidth {
      get { return settings.layersSplitter; }
    }

    public int ActionsGridSplitterWidth {
      get { return settings.actionsSplitter; }
    }

    public MainWindow() {
      var root = Paths.AppDataDirectory();
      Loaded += OnLoaded;
      DataContext = this;
      LoadApplicationSettings();
      InitializeComponent();
      WindowStartupLocation = WindowStartupLocation.Manual;
      Left = settings.x;
      Top = settings.y;
      Height = settings.height;
      Width = settings.width;
    }

    public void OnLoaded(object sender, EventArgs e) {
      if (settings.fullScreen) {
        if (IsLoaded) WindowState = WindowState.Maximized;
      }
      if (Project.layers.Count > 0) {
        Project.selectedLayer = Project.layers[0];
      }
      EventBus.OnProjectInitialized(this);
    }

    public void OnClosed(object sender, EventArgs e) {
      SaveApplicationSettings();
    }

    public static void OnPropertyChanged(PropertyChangedEventHandler propertyChanged, object sender, string propertyName) {
      propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
    }

    public static void LoadApplicationSettings() {
      settings = new ApplicationSettings();
      try {
        settings.Load();
      } catch {
        Debug.Log("couldn't load");
      }
    }

    public static void SaveApplicationSettings() {
      var mainWindow = Application.Current.MainWindow;
      var layersGridSplitter = (ColumnDefinition)mainWindow.FindName("layersGridSplitter");
      var actionsGridSplitter = (ColumnDefinition)mainWindow.FindName("actionsGridSplitter");
      settings.height = Convert.ToInt32(mainWindow.Height);
      settings.width = Convert.ToInt32(mainWindow.Width);
      settings.x = Convert.ToInt32(mainWindow.Left);
      settings.y = Convert.ToInt32(mainWindow.Top);
      settings.fullScreen = mainWindow.WindowState == WindowState.Maximized;
      settings.layersSplitter = Convert.ToInt32(layersGridSplitter.Width.Value);
      settings.actionsSplitter = Convert.ToInt32(actionsGridSplitter.Width.Value);
      settings.Save();
    }

    public static void EnableAutoSave(bool enabled) {
      settings.autoSave = enabled;
      SaveApplicationSettings();
    }
  }
}
