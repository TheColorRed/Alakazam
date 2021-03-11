using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Alakazam.Engine;
using Alakazam.Engine.Export;
using Alakazam.Engine.Import;
using Alakazam.Engine.Events;
using Action = Alakazam.Plugin.Action;
using Alakazam.Editor;
using System.ComponentModel;

namespace Alakazam.Controls {
  public partial class MainMenuControl : UserControl {

    private readonly Project project = MainWindow.project;

    private Layer SelectedLayer { get { return project.selectedLayer; } }

    public bool AutoSaveEnabled { get { return MainWindow.settings.autoSave; } }

    public MainMenuControl() {
      DataContext = this;
      InitializeComponent();
      LoadPluginMenus();
    }

    public void OnExit(object sender, EventArgs evt) {
      System.Windows.Application.Current.Shutdown();
    }

    public void OnOpenAsLayer(object sender, EventArgs evt) {
      var openFileDialog = new System.Windows.Forms.OpenFileDialog();
      var result = openFileDialog.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK) {
        project.AddLayer(openFileDialog.FileName);
      }
    }

    public void OnNewEmptyLayer(object sender, EventArgs evt) {
      project.AddEmptyLayer();
    }

    public void OnNewNoiseLayer(object sender, EventArgs evt) {
      project.AddNoiseLayer();
    }

    public void OnLayerDelete(object sender, EventArgs evt) {
      project.selectedLayer.Delete();
    }

    public void OnSaveProject(object sender, EventArgs evt) {
      project.Save();
    }

    public void OnExportProject(object sender, EventArgs evt) {
      var saveFileDialog = new System.Windows.Forms.SaveFileDialog {
        DefaultExt = "ala",
        Filter = "Alakazam|*.ala",
        CheckFileExists = true
      };
      var result = saveFileDialog.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK) {
        new ExportProject(project).Export(saveFileDialog.FileName);
      }
    }

    public void OnImportProject(object sender, EventArgs evt) {
      var openFileDialog = new System.Windows.Forms.OpenFileDialog {
        DefaultExt = "ala",
        Filter = "Alakazam|*.ala",
        CheckFileExists = true
      };
      var result = openFileDialog.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK) {
        new ImportProject().Import(openFileDialog.FileName);
      }
    }

    public void OnAutoSave(object sender, EventArgs evt) {
      MainWindow.EnableAutoSave(menuAutoSave.IsChecked);
    }

    public void OnSaveWindowLayout(object sender, EventArgs evt) {
      MainWindow.SaveApplicationSettings();
    }

    public void OnFilterAdd(object sender, EventArgs evt) {
      var menuItem = (MenuItem)sender;
      var tag = menuItem.Tag.ToString();
      var classPath = string.Format("Alakazam.Filters.{0}", tag);
      AddAction(classPath);
    }

    public void OnColorAdd(object sender, EventArgs evt) {
      var menuItem = (MenuItem)sender;
      var tag = menuItem.Tag.ToString();
      var classPath = string.Format("Alakazam.Colors.{0}", tag);
      AddAction(classPath);
    }

    public void OnPropertyAdd(object sender, EventArgs evt) {
      var menuItem = (MenuItem)sender;
      var tag = menuItem.Tag.ToString();
      var classPath = string.Format("Alakazam.Properties.{0}", tag);
      AddAction(classPath);
    }

    private void AddAction(string classPath) {
      var instance = (Action)Activator.CreateInstance("Color", classPath).Unwrap();
      SelectedLayer.actions.Add(instance);
      SelectedLayer.ApplyActions();
      EventBus.OnLayerActionAdded(this);
    }

    private void LoadPluginMenus() {
      var pluginMenuItems = Plugins.GetMenuItems();
      foreach (var pluginMenuItem in pluginMenuItems) {
        var segments = Regex.Split(pluginMenuItem.path, @"(?<!\\)/");
        AddMenuItem(segments, pluginMenuItem);
      }
    }

    private void AddMenuItem(string[] segments, MenuItemAction action, int depth = 0, MenuItem menuItem = null) {
      var segment = segments[depth].Replace(@"\", "");
      dynamic menuItm = (object)menuItem ?? mainMenu;
      var childMenu = GetChildMenuItem(menuItm, segment);
      if (childMenu != null && depth + 1 < segments.Length) {
        AddMenuItem(segments, action, depth + 1, childMenu);
        return;
      } else {
        var newItem = new MenuItem { Header = segment, DataContext = action };
        if (depth + 1 < segments.Length) {
          menuItm.Items.Add(newItem);
          // newItem.Items.SortDescriptions.Add()
          AddMenuItem(segments, action, depth + 1, newItem);
        } else {
          int addedIndex = -1;
          int itemCount = menuItm.Items.Count;
          // int itidx = (int)(action.order.GetValue(0) ?? 0);
          // var idx = itemCount < itidx ? -1 : action.order[0];
          // if (idx > -1) menuItm.Items.Insert(idx, newItem);
          // else
          addedIndex = menuItm.Items.Add(newItem);

          // if (itemCount > 1) {
          //   var separatorAttr = Attribute.GetCustomAttribute(action.action, typeof(MenuItemSeparatorAttribute));
          //   if (separatorAttr != null) {
          //     var separator = new Separator();
          //     // menuItm.Items.Insert(addedIndex > -1 ? addedIndex : idx, separator);
          //   }
          // }
          newItem.Click += (sender, evt) => {
            var menuItem = (MenuItemAction)((MenuItem)sender).DataContext;
            var instance = (Action)Activator.CreateInstance(menuItem.action);
            SelectedLayer.actions.Add(instance);
            SelectedLayer.ApplyActions();
            EventBus.OnLayerActionAdded(this);
          };
        }
      }
    }

    private MenuItem GetChildMenuItem(Menu current, string name) {
      foreach (var child in current.Items) {
        if (child is MenuItem childItem && (string)childItem.Header == name) {
          return childItem;
        }
      }
      return null;
    }

    private MenuItem GetChildMenuItem(MenuItem current, string name) {
      foreach (var child in current.Items) {
        if (child is MenuItem childItem && (string)childItem.Header == name) {
          return childItem;
        }
      }
      return null;
    }
  }
}