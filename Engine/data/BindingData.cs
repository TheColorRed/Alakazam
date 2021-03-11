using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using Alakazam.Plugin;

namespace Alakazam.Engine {
  public class BindingData {
    public Control Input { get; set; }
    public List<Control> Controls { get; set; } = new List<Control>();
    public TextBlock Label { get; set; }
    public PropertyInfo Property { get; set; }
    public FieldInfo Field { get; set; }
    public Action Action { get; set; }
    public Layer Layer { get; set; }
    public object Value { get; set; }
  }
}