
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Alakazam.Engine.Events;
using Alakazam.ImageMagick;
using Alakazam.Plugin;
using ImageMagick;

namespace Alakazam.Engine {
  public partial class Layer {
    public void ApplyActions(double value = 0) {
      processingValue = value;
      if (applyingActions) return;
      applyingActions = true;
      actionProcessingIndex = -1;
      EventBus.OnLayerActionProcessChanged(this);
      cancelSource?.Cancel();
      cancelSource = new CancellationTokenSource();
      Task.Run(() => {
        using var clone = (MagickImage)Image.Clone();
        try {
          foreach (var command in actions) {
            cancelSource.Token.ThrowIfCancellationRequested();
            actionProcessingIndex++;
            if (!command.Enabled) continue;
            EventBus.OnLayerActionProcessChanged(this);

            // If the action has a mask associated with it set the mask.
            if (command.ActionMask?.image is MagickImage image) {
              clone.SetWriteMask(image);
            }

            // Apply the different actions to the image.
            if (command is Filter filter) {
              filter.Apply(clone);
            } else if (command is Color color) {
              color.Apply(clone);
            } else if (command is Property property) {
              property.Apply(clone);
            }

            // Remove an action masks that have been applied.
            clone.RemoveWriteMask();
          }
        } catch {
          ApplyActions(processingValue);
          return;
        }

        actionProcessingIndex = -1;
        EventBus.OnLayerActionProcessChanged(this);

        // Apply rotation.
        if (transform.Rotation != 0) {
          clone.Rotate(transform.Rotation);
        }

        // Apply scale.
        if (transform.Scale.x != 100 || transform.Scale.y != 100) {
          clone.Scale(new Percentage(transform.Scale.x), new Percentage(transform.Scale.y));
        }

        // Set the new filtered image for the layer.
        FilteredImage = (MagickImage)clone.Clone();
        UpdatePreview();

        applyingActions = false;
        if (value != processingValue) {
          ApplyActions(processingValue);
        }

      }, cancelSource.Token);
    }

    public void UpdatePreview() {
      var clone = (MagickImage)FilteredImage.Clone();
      LayerPreview = clone.GetImagePreview(this, new Size(0, 60));
    }

    public void SelectLayer() {
      Project.selectedLayer = this;
      EventBus.OnLayerSelectionChanged(this);
    }

    public void Delete() {
      cancelSource?.Cancel();
      var filePath = Paths.GetBaseLayerPath(Project, this);
      if (File.Exists(filePath)) File.Delete(filePath);
      Project.layers.Remove(this);
      EventBus.OnLayerDeleted(this);
      Project.AutoSave();
    }

    public void AddLayerMask(LayerMask layerMask) {
      LayerMask = layerMask;
      layerMask.Save(Project);
      Project.AutoSave();
    }

    public void RemoveLayerMask() {
      LayerMask.Delete(Project);
      LayerMask = null;
      Project.AutoSave();
    }
  }
}