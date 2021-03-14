using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Alakazam.Engine.Events;
using Alakazam.Plugin;
using Alakazam.ImageMagick;
using ImageMagick;
using Newtonsoft.Json;

namespace Alakazam.Engine {
  public partial class Layer {

    public string Name { get; set; } = "Untitled";
    public Transform transform = new Transform();

    public bool visible = true;
    public double opacity = 100;
    public CompositeOperator blendType = CompositeOperator.SrcOver;
    public ObservableCollection<Action> actions = new ObservableCollection<Action>();

    [JsonProperty]
    protected string fileName;
    private BitmapImage layerPreviewBitmap;
    protected MagickImage imageOriginal;
    private MagickImage filteredImage;
    private bool applyingActions = false;
    private double processingValue = -1;
    private CancellationTokenSource cancelSource = new CancellationTokenSource();
    public int actionProcessingIndex = -1;

    [JsonConstructor] public Layer(string fileName) => this.fileName = fileName;
    public Layer(Project project) => Project = project;
    public Layer(string fileName, Project project) {
      this.fileName = fileName;
      Project = project;
    }

    [JsonIgnore] public string FileName => fileName;
    [JsonIgnore] public string BasePath => Paths.GetBaseLayerPath(Project, this);
    [JsonIgnore] public int ActionProcessingIndex => actionProcessingIndex;
    [JsonIgnore] public Project Project { get; set; }
    [JsonIgnore] public string LayerVisibilityIcon => visible ? "Eye" : "EyeSlash";

    /// <summary>
    /// An optional layer mask.
    /// </summary>
    public LayerMask? LayerMask { get; set; }

    /// <summary>
    /// The original image before anything has been applied.
    /// </summary>
    [JsonIgnore]
    public MagickImage Image {
      get {
        if (imageOriginal == null) {
          if (File.Exists(BasePath)) {
            imageOriginal = new MagickImage(BasePath) {
              // BackgroundColor = MagickColors.Transparent
            };
          } else {
            imageOriginal = new MagickImage(MagickColors.Transparent, Project.size.width, Project.size.height);
          }
          imageOriginal.Settings.Format = MagickFormat.Bmp;
          ApplyActions();
        }
        return imageOriginal;
      }
      set => imageOriginal = value;
    }

    /// <summary>
    /// The image after all filters and actions have been applied.
    /// </summary>
    [JsonIgnore]
    public MagickImage FilteredImage {
      get => filteredImage ?? Image;
      set {
        filteredImage = value;
        EventBus.OnLayerFiltered(this);
      }
    }

    // [JsonIgnore]
    // public Position Origin {
    //   get {
    //     var width = imageOriginal.Width;
    //     var height = imageOriginal.Height;
    //     return new Position(width / 2, height / 2);
    //   }
    // }

    [JsonIgnore]
    public BitmapImage LayerPreview {
      get {
        if (layerPreviewBitmap == null) layerPreviewBitmap = FilteredImage.ToBitmapImage();
        return layerPreviewBitmap;
      }
      set {
        layerPreviewBitmap = value;
        EventBus.OnLayerPreviewChanged(this);
      }
    }
  }
}