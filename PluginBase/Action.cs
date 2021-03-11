using System;
using ImageMagick;

namespace Alakazam.Plugin {

  public class DrawNotImplementedException : System.Exception {
    public DrawNotImplementedException() : base("Method not Implemented.") { }
  }

  public struct ActionMask {
    public MagickImage image;

    public ActionMask(MagickImage image) {
      this.image = image;
    }
  }

  public abstract class Action {
    /// <summary>
    /// The actions displayname.
    /// </summary>
    public abstract string Name { get; }
    /// <summary>
    /// Whether or not the action is collapsed in the editor.
    /// </summary>
    public bool Collapsed { get; set; } = false;
    /// <summary>
    /// A mask that will be used for applying the results.
    /// </summary>
    public ActionMask? ActionMask { get; set; }
    /// <summary>
    /// Whether or not the action is activated.<br/>
    /// Inactive actions will not generate output in the editor or when exported
    /// to an image file such as a png.
    /// </summary>
    public bool Enabled { get; set; } = true;
    /// <summary>
    /// Draws elements in the inspecter to expose properties.
    /// </summary>
    public virtual void Draw() {
      throw new DrawNotImplementedException();
    }
  }

  public abstract class Filter : Action {
    /// <summary>
    /// Applies a filer to the current state of the image.
    /// </summary>
    /// <param name="image">The image that will be manipulated by the filter.</param>
    public abstract void Apply(MagickImage image);
  }

  public abstract class Color : Action {
    /// <summary>
    /// Applies a colorization to the current state of the image.
    /// </summary>
    /// <param name="image">The image that will be colorized.</param>
    public abstract void Apply(MagickImage image);
  }

  public abstract class Property : Action {
    /// <summary>
    /// Applies a property manipulation to the current state of the image.
    /// </summary>
    /// <param name="image">The image that will have a property applied to it.</param>
    public abstract void Apply(MagickImage image);
  }
}
