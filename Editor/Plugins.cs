using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alakazam.Editor;
using Alakazam.Plugin;
using Type = System.Type;

namespace Alakazam.Engine {

  public struct MenuItemAction {
    public readonly string path;
    public readonly int[] order;
    public readonly Type action;

    public MenuItemAction(string path, int[] order, Type action) {
      this.path = path;
      this.action = action;
      this.order = order;
    }
  }

  public class Plugins {
    public static List<Assembly> plugins = new List<Assembly>();

    public static void Load() {
      var pluginPaths = GetPluginPaths();

      foreach (var plugin in pluginPaths) {
        Assembly pluginAssembly = LoadPlugin(plugin);
        AddPlugin(pluginAssembly);
      }
    }

    internal static void AddPlugin(Assembly plugin) {
      plugins.Add(plugin);
    }

    public static List<MenuItemAction> GetMenuItems() {
      var results = new List<MenuItemAction>();
      foreach (var plugin in plugins) {
        var types = plugin.GetTypes();
        foreach (var type in types) {
          if (
            typeof(Color).IsAssignableFrom(type) ||
            typeof(Filter).IsAssignableFrom(type) ||
            typeof(Property).IsAssignableFrom(type)
          ) {
            var menuItem = type.GetCustomAttribute<MenuItemAttribute>();
            if (menuItem != null) {
              results.Add(new MenuItemAction(menuItem.path, menuItem.order, type));
            }
          }
        }
      }
      return results;
    }

    public static string[] GetPluginPaths() {
      var root = GetRoot();
      var pluginPath = Path.Combine(root, "Plugins");
#if DEBUG
      var pluginPaths = Glob.Find(pluginPath, "*/bin/Debug/*/*.dll");
#else
      var pluginPaths = Glob.Find(pluginPath, "*/bin/Release/*/*.dll");
#endif

      var plugins = Paths.Plugins();
      var userPlugins = Glob.Find(plugins, "*.dll");
      var origLen = pluginPaths.Length;
      System.Array.Resize(ref pluginPaths, origLen + userPlugins.Length);
      System.Array.Copy(userPlugins, 0, pluginPaths, origLen, userPlugins.Length);

      return pluginPaths;
    }

    internal static string GetRoot() {
      return Path.GetFullPath(Path.Combine(
        Path.GetDirectoryName(
          Path.GetDirectoryName(
            Path.GetDirectoryName(
              Path.GetDirectoryName(
                Path.GetDirectoryName(typeof(Plugins).Assembly.Location)))))));
    }

    internal static Assembly LoadPlugin(string relativePath) {
      var root = GetRoot();
      string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
      Debug.Log($"Loading commands from: {pluginLocation}");
      PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
      return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
    }
  }
}