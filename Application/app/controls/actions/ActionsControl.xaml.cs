using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Alakazam.Dialogs;
using Alakazam.Editor;
using Alakazam.Engine;
using Alakazam.Engine.Events;
using Alakazam.ImageMagick;
using Alakazam.Plugin;
using ImageMagick;
using Action = Alakazam.Plugin.Action;

namespace Alakazam.Controls {

  enum DragDirection { None, Left, Right }

  public partial class ActionsControl : UserControl {

    public readonly ItemsControl actions = new ItemsControl();
    public static DockPanel panel;

    private readonly Project project = MainWindow.project;
    private readonly Window mainWindow = Application.Current.MainWindow;
    // public static BindingData bindingData;
    public static bool isDragValue = false;
    private double lastPositionX = 0;
    private DragDirection direction = DragDirection.None;

    public Engine.Transform Transform => MainWindow.project.selectedLayer.transform;

    public ActionsControl() {
      DataContext = this;
      // actions = actions;
      InitializeComponent();
      stackPanel.Children.Add(actions);
      EventBus.LayerSelectionChanged += OnBuildActions;
      EventBus.ProjectInitialized += OnBuildActions;
      EventBus.LayerActionAdded += OnBuildActions;
      GUILayout.ActionsUpdated += OnBuildActions;
      EventBus.LayerActionProcessChanged += OnUpdateProgressVisibility;
      EventBus.OpenColorPicker += OnOpenColorPicker;

      mainWindow.MouseUp += (sender, evt) => {
        if (isDragValue) {
          GUILayout.ActiveControl = null;
          isDragValue = false;
        }
      };

      mainWindow.MouseLeave += (sender, evt) => {
        if (evt.LeftButton == MouseButtonState.Pressed && GUILayout.ActiveControl != null) {
          var current = evt.GetPosition(mainWindow);
          var appX = mainWindow.Left;
          var appW = mainWindow.Width;
          Debug.Log(current.X, appW);
          double x = 0, y = current.Y;
          if (current.X > appW) {
            Debug.Log("Right");
          } else if (current.X <= 20) {
            Debug.Log("Here");
            x = appX + appW;
          }
          System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)x, (int)y);
        }
      };
      mainWindow.MouseMove += (sender, evt) => {
        if (evt.LeftButton == MouseButtonState.Pressed && GUILayout.ActiveControl != null) {
          double deltaDirection = evt.GetPosition(mainWindow).X - lastPositionX;
          if (deltaDirection == 0) direction = 0;
          else direction = deltaDirection < 0 ? DragDirection.Left : DragDirection.Right;
          lastPositionX = evt.GetPosition(mainWindow).X;
          // if (direction == DragDirection.Right) {
          //   ModifyValue.IncrementValue(GUILayout.ActiveControl);
          // } else if (direction == DragDirection.Left) {
          //   ModifyValue.DecrementValue(GUILayout.ActiveControl);
          // }
          // EventBus.OnLayerActionChanged(this);
        } else {
          lastPositionX = evt.GetPosition(mainWindow).X;
        }
      };
    }

    public void OnOpenColorPicker(object sender, EventArgs evt) {
      if (sender is Button button) {
        var dialog = new ColorDialog {
          ResizeMode = ResizeMode.NoResize,
          Owner = Application.Current.MainWindow
        };
        dialog.Color = (MagickColor)button.DataContext;
        dialog.ShowDialog();
        if (dialog.DialogResult == true) {
          Debug.Log(dialog.Color);
          // button.Background =
        }
      }
    }

    public void OnBuildActions(object sender, EventArgs evt) {
      Dispatcher.BeginInvoke(new System.Action(() => Build()));
    }

    private void OnUpdateProgressVisibility(object sender, EventArgs evt) {
      actions.Dispatcher.BeginInvoke(new System.Action(() => {
        var processingIndex = project.selectedLayer.ActionProcessingIndex;
        actions.ApplyTemplate();

        var progressBars = actions.Items.OfType<ProgressBar>();
        foreach (var bar in progressBars) {
          bar.Visibility = Visibility.Hidden;
        }

        var item = (ProgressBar)LogicalTreeHelper
          .FindLogicalNode(actions, $"ActionProgress_{processingIndex + 1}");

        if (item == null || processingIndex == -1) return;
        item.Visibility = Visibility.Visible;
      }));
    }

    private void Build() {
      var layer = project.selectedLayer;
      var layerActions = layer.actions;
      actions.Items.Clear();
      GUILayout.Reset();
      GUILayout.actionPropertyIndex = 0;

      // Draw layer specific properties.
      if (layer is LayerNoise layerNoise) {
        GUILayout.PropertyField(new SerializedProperty(layerNoise, "IsColor"));
        var btn = GUILayout.Button("Regenerate Noise");
        btn.Click += (sender, evt) => {
          layerNoise.GenerateImage();
          EventBus.OnLayerActionChanged(sender);
        };
      }

      // Draw the required primary transform.
      GUILayout.PropertyField(new SerializedProperty(Transform, "IsAnchored"));

      if (Transform.IsAnchored)
        GUILayout.PropertyField(new SerializedProperty(Transform, "Anchor"));
      else GUILayout.PropertyField(new SerializedProperty(Transform, "Position"));

      GUILayout.PropertyField(new SerializedProperty(Transform, "Rotation"));
      GUILayout.PropertyField(new SerializedProperty(Transform, "Scale"));

      // Call the draw method on all the layer actions (excluding the transform).
      foreach (var action in layerActions) {
        try {
          action.Draw();
        } catch (DrawNotImplementedException) {
          GUILayout.HandleNoDrawAction(action);
        }
      }

      // Add the the actions to the panel to be physically drawn to the screen.
      int index = 0;
      foreach (var guiAction in GUILayout.Actions) {
        var processingIndex = project.selectedLayer.ActionProcessingIndex;
        if (guiAction.Header != null)
          actions.Items.Add(guiAction.Header);
        guiAction.Layer = project.selectedLayer;
        foreach (var panel in guiAction.panels) {
          if (guiAction.action == null || !guiAction.action.Collapsed) {
            actions.Items.Add(panel);
          }
        }
        if (guiAction.action != null && index > 0) {
          var progressBar = new ProgressBar {
            IsIndeterminate = true,
            Margin = new Thickness(0, 5, 0, 0),
            Visibility = Visibility.Hidden,
            Name = $"ActionProgress_{index}"
          };
          actions.Items.Add(progressBar);
        }
        if (guiAction.action != null && index < GUILayout.Actions.Count - 1) {
          var top = guiAction.action.Collapsed ? 0 : 10;
          var separator = new Separator {
            Margin = new Thickness(0, top, 0, 0)
          };
          actions.Items.Add(separator);
        }
        index++;
      }
    }
  }
}