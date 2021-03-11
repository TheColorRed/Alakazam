using System.IO;

namespace Alakazam.Engine {
  public partial class Paths {

    private static readonly string layerExt = ".tiff";

    /// <summary>
    /// Gets the path to a layer.
    /// </summary>
    /// <param name="project">The project the layer relates to.</param>
    /// <param name="layer">The layer to get the path for.</param>
    /// <returns>The path to the layer. The layer will contain all manipulations.</returns>
    public static string GetLayerPath(Project project, Layer layer) {
      if (project == null) return "";
      return GetLayerPath(project.guid, layer);
    }

    /// <summary>
    /// Gets the path to a layer using the name of the project.
    /// </summary>
    /// <param name="projectName">The project the layer relates to.</param>
    /// <param name="layer">The layer to the the path for.</param>
    /// <returns>The path to the layer. The layer will contain all manipulations.</returns>
    public static string GetLayerPath(string projectName, Layer layer) {
      var projRoot = ProjectRootPath(projectName);
      return Path.Combine(projRoot, "layers", (layer.FileName ?? "") + layerExt);
    }

    /// <summary>
    /// Gets the original layer.
    /// </summary>
    /// <param name="project">The project the layer relates to.</param>
    /// <param name="layer">The layer to the the path for.</param>
    /// <returns>The path to the layer. The layer will be the original image without manipulations.</returns>
    public static string GetBaseLayerPath(Project project, Layer layer) {
      if (project == null) return "";
      return GetBaseLayerPath(project.guid, layer);
    }

    /// <summary>
    /// Gets the path to the original layer using the name of the project.
    /// </summary>
    /// <param name="projectName">The project the layer relates to.</param>
    /// <param name="layer">The layer to the the path for.</param>
    /// <returns>The path to the layer. The layer will be the original image without manipulations.</returns>
    public static string GetBaseLayerPath(string projectName, Layer layer) {
      var resources = ResourcesPath(projectName);
      return Path.Combine(resources, "bases", layer.FileName ?? "");
    }
  }
}