
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
        var clone = (MagickImage)Image.Clone();

        // Apply scale.
        if (transform.Scale.x != 100 || transform.Scale.y != 100) {
          clone.Scale(new Percentage(transform.Scale.x), new Percentage(transform.Scale.y));
        }

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

            clone.RemoveWriteMask();
            clone.RemoveReadMask();
            clone.RemoveRegionMask();

            // Remove an action masks that have been applied.
            clone.RemoveWriteMask();
          }
        } catch (MagickException e) {
          Debug.Log(e);
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

        // Set a mask on the layer if there is one.
        if (LayerMask?.Image is MagickImage layerMask) {
          var mask = layerMask.Clone();
          // var filtered = FilteredImage.Clone();
          var image = new MagickImage(MagickColors.Transparent, clone.Width, clone.Height);

          image.Composite(clone, CompositeOperator.Over);

          if (transform.Scale.x != 100 || transform.Scale.y != 100)
            mask.Scale(new Percentage(transform.Scale.x), new Percentage(transform.Scale.y));
          mask.Alpha(AlphaOption.Off);
          mask.Negate();

          image.Composite(mask, CompositeOperator.CopyAlpha);

          clone = (MagickImage)image.Clone();
        }

        // Set the new filtered image for the layer.
        FilteredImage = (MagickImage)clone.Clone();
        UpdatePreview();

        applyingActions = false;
        if (value != processingValue) {
          ApplyActions(processingValue);
        }
        clone.Dispose();
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
      // Cancel any actions that might be happening.
      cancelSource?.Cancel();

      // Delete the file on disk.
      var filePath = Paths.GetBaseLayerPath(Project, this);
      if (File.Exists(filePath)) File.Delete(filePath);

      // Delete the filemask on disk.
      LayerMask?.Delete(Project);

      // Remove the layer from the project.
      Project.layers.Remove(this);

      // Notify the application of the layer deletion
      // and then save the project.
      EventBus.OnLayerDeleted(this);
      Project.AutoSave();
    }

    public void AddLayerMask(LayerMask layerMask) {
      LayerMask = layerMask;
      layerMask.Save(Project);
      ApplyActions();
      Project.AutoSave();
      EventBus.OnLayerMaskChanged(this);
    }

    public void RemoveLayerMask() {
      LayerMask.Delete(Project);
      LayerMask = null;
      ApplyActions();
      Project.AutoSave();
      EventBus.OnLayerMaskChanged(this);
    }
  }
}