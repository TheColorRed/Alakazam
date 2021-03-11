namespace Alakazam.Engine.Settings {
  public class ProjectSettings : SettingsObject {

    public ProjectSettings() { }

    public void Save(Project project) {
      var projectFilePath = Paths.ProjectFile(project);
      base.Save(projectFilePath, project);
    }

    public new void Save(string guid, object data) {
      var projectFilePath = Paths.ProjectFile(guid);
      base.Save(projectFilePath, data);
    }

    public Project Load(string projectName) {
      var projectFilePath = Paths.ProjectFile(projectName);
      var project = base.Load<Project>(projectFilePath);
      foreach (var layer in project.layers) {
        layer.Project = project;
      }
      return project;
    }

  }
}