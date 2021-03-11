using System.Collections.ObjectModel;
using ImageMagick;
using System;
using Newtonsoft.Json;
using Alakazam.Engine.Events;
using Alakazam.Engine.Settings;
using System.Windows.Input;
using System.Windows.Controls;
using Alakazam.ImageMagick;

namespace Alakazam.Engine {
  public class Project {
    public string name;
    public string guid;
    public Size size;
    // public Density density = new Density(120, 120);
    public ObservableCollection<Layer> layers;

    [JsonIgnore]
    public MagickImage checkerBoardLarge;
    [JsonIgnore]
    public MagickImage checkerBoardSmall;

    [JsonIgnore]
    public Layer selectedLayer;

    [JsonIgnore]
    public ApplicationSettings ApplicationSettings {
      get {
        if (applicationSettings == null) {
          applicationSettings = new ApplicationSettings();
          applicationSettings.Load();
        }
        return applicationSettings;
      }
    }
    private ApplicationSettings applicationSettings;

    public Project() {
      size = new Size(800, 600);
      guid = Guid.NewGuid().ToString();
      InitializeProject();
    }

    [JsonConstructor]
    public Project(string name, Size size) {
      this.name = name;
      this.size = size;
      InitializeProject();
    }

    private void InitializeProject() {
      selectedLayer = new EmptyLayer(this);
      layers = new ObservableCollection<Layer>();
      checkerBoardLarge = size.GetCheckerBoard(90);
      checkerBoardSmall = size.GetCheckerBoard(10);
      EventBus.LayerActionChanged += OnLayerActionChanged;
      EventBus.LayerActionChanged += OnAutoSave;
      EventBus.LayerActionAdded += OnAutoSave;
      EventBus.LayerActionRemoved += OnAutoSave;
      EventBus.LayerBlendChanged += OnAutoSave;
    }

    private void OnLayerActionChanged(object sender, EventArgs evt) {
      var value = 0.0;
      if (Keyboard.FocusedElement is Slider slider) {
        value = slider.Value;
      } else if (Keyboard.FocusedElement is TextBox textBox) {
        value = Convert.ToDouble(textBox.Text);
      }
      selectedLayer.ApplyActions(value);
    }

    private void OnAutoSave(object sender, EventArgs evt) {
      AutoSave();
    }

    public void AddLayer() {
      var layer = new Layer(this);
      layers.Add(layer);
      layer.UpdatePreview();
      layer.SelectLayer();
      AutoSave();
    }

    public void AddLayer(string filePath) {
      var layer = new LayerImage(filePath, this);
      layers.Add(layer);
      layer.UpdatePreview();
      layer.SelectLayer();
      AutoSave();
    }

    public void AddNoiseLayer() {
      var layer = new LayerNoise(this);
      layers.Add(layer);
      layer.UpdatePreview();
      layer.SelectLayer();
      AutoSave();
    }

    public void AddEmptyLayer() {
      var layer = new Layer(this);
      layers.Add(layer);
      layer.SelectLayer();
      AutoSave();
    }

    public void AutoSave() {
      if (ApplicationSettings.autoSave) {
        Save();
      }
    }

    public void Save() {
      new ProjectSettings().Save(this);
    }

  }
}