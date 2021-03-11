using Alakazam.Engine;
using ImageMagick;

namespace Alakazam.Editor {
  public static partial class GUILayout {

    public static void PropertyField(SerializedProperty property) {
      if (property == null) return;

      if (property.Object == null) { Debug.Warn("Associated object is missing."); return; }
      if (property.Property == null) {
        Debug.Warn($"Associated property \"{property.Name}\" is missing from \"{property.Object.GetType().Name}\".");
        return;
      }

      if (property.Object.GetType().GetProperty(property.Property.Name) == null) return;

      if (
        property.PropertyType == typeof(double) ||
        property.PropertyType == typeof(int)
      ) {
        InputBox(property);
      } else if (property.PropertyType == typeof(bool)) {
        ToggleBox(property);
      } else if (property.PropertyType.IsEnum) {
        ComboBox(property);
      } else if (property.Value is Vector) {
        VectorInput(property);
      } else if (property.Value is MagickColor) {
        ColorInput(property);
      }

    }
  }
}