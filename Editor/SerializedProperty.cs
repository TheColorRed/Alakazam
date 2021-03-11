using System;
using System.Reflection;

namespace Alakazam.Editor {
  public class SerializedProperty {

    private readonly object obj;
    private readonly PropertyInfo property;
    private readonly string name;

    public string Name => name;
    public object Object => obj;
    public Type PropertyType => Value.GetType();
    public PropertyInfo Property => property;
    public object Value => property.GetValue(obj);

    public SerializedProperty(object obj, string propertyName) {
      this.obj = obj;
      name = propertyName;
      property = obj.GetType()
      .GetProperty(
        propertyName,
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
      );
    }
  }
}