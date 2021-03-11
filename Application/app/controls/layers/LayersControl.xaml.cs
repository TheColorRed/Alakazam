using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;
using Alakazam.Engine.Events;
using Alakazam.Engine;

namespace Alakazam.Controls {
  public partial class LayersControl : UserControl {
    // public event PropertyChangedEventHandler PropertyChanged;

    public readonly Project project = MainWindow.project;

    public ObservableCollection<Layer> Layers {
      get { return project.layers; }
    }

    public LayersControl() {
      // base();
      DataContext = this;
      InitializeComponent();
      EventBus.LayerPreviewChanged += OnLayerPreviewChanged;
      // EventBus.LayerSelectionInitialized += OnLayerSelectionInitialized;
      // EventBus.LayerPreviewChanged += OnPreviewChanged;
      layerList.SelectionChanged += OnLayerSelectionChanged;
      EventBus.LayerSelectionChanged += OnLayerSelectionChanged;
    }

    public void OnLayerPreviewChanged(object sender, EventArgs evt) {
      Dispatcher.Invoke(() => layerList.Items.Refresh());
    }

    // public void OnPreviewChanged(object sender, EventArgs e) {
    //   Dispatcher.Invoke(() => layerList.Items.Refresh());
    // }

    // public void OnLayerSelectionInitialized(object sender, EventArgs e) {
    //   layerList.SelectedIndex = 0;
    //   // project.selectedLayer = (Layer)layerList.SelectedItem;
    //   // EventBus.OnLayerSelectionChanged(this);
    // }

    private void OnLayerSelectionChanged(object source, EventArgs evt) {
      Dispatcher.Invoke(() => {
        layerList.SelectedItem = project.selectedLayer;
        layerList.Items.Refresh();
      });
    }

    private void OnLayerSelectionChanged(object source, SelectionChangedEventArgs evt) {
      var selected = ((ListBox)source).SelectedItem as Layer;
      if (selected is Layer) {
        project.selectedLayer = selected;
        EventBus.OnLayerSelectionChanged(this);
      }
    }

    public void OnToggleLayerVisibility(object source, EventArgs e) {
      var button = (Button)source;
      var layer = (Layer)button.DataContext;
      layer.visible = !layer.visible;
      layerList.Items.Refresh();
      EventBus.OnLayerVisibilityChanged(this);
    }

  }
}