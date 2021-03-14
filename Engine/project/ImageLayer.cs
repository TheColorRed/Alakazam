// using System;
// using System.Collections.ObjectModel;
// using System.IO;
// using System.Threading.Tasks;
// using System.Windows.Media.Imaging;
// using Alakazam.Events;
// using Alakazam.Filters;
// using Alakazam.Properties;
// using ImageMagick;
// using Newtonsoft.Json;

// namespace Alakazam {
//   public class ImageLayer : Layer {

//     public readonly string fileName;
//     public readonly Transform transform;

//     public CompositeOperator blendType = CompositeOperator.SrcOver;
//     public double opacity = 100;
//     public ObservableCollection<AlakazamAction> actions;
//     public bool visible = true;

//     [JsonProperty]
//     private string title = "Untitled";
//     private MagickImage? imageOriginal;
//     private MagickImage? filteredImage;
//     private BitmapImage? layerPreviewBitmap;

//     [JsonConstructor]
//     public ImageLayer(string fileName, string title = "Untitled") {
//       this.fileName = fileName;
//       this.title = title;
//       transform = new Transform();
//       this.actions = new ObservableCollection<AlakazamAction>();
//     }

//     public ImageLayer() {
//       this.fileName = "";
//       this.title = "Untitled";
//       transform = new Transform();
//       this.actions = new ObservableCollection<AlakazamAction>();
//     }

//     [JsonIgnore]
//     public string Name {
//       get { return title; }
//       set { title = value; }
//     }

//     [JsonIgnore]
//     public string LayerPath {
//       get { return Helpers.GetLayerPath(Engine.Engine.project, this); }
//     }

//     [JsonIgnore]
//     public string BasePath {
//       get { return Helpers.GetBaseLayerPath(Engine.Engine.project, this); }
//     }

//     [JsonIgnore]
//     public Position Origin {
//       get {
//         var width = imageOriginal.Width;
//         var height = imageOriginal.Height;
//         return new Position(width / 2, height / 2);
//       }
//     }

//     [JsonIgnore]
//     public BitmapImage LayerPreview {
//       get {
//         if (layerPreviewBitmap == null) layerPreviewBitmap = FilteredImage.ToBitmapImage();
//         return layerPreviewBitmap;
//       }
//       set {
//         layerPreviewBitmap = value;
//         EventBus.OnLayerPreviewChanged(this);
//       }
//     }

//     [JsonIgnore]
//     public string LayerVisibilityIcon {
//       get {
//         return visible ? "Eye" : "EyeSlash";
//       }
//     }
//   }

//   public class EmptyLayer : Layer {
//     public EmptyLayer() : base("", "") { }
//   }
// }