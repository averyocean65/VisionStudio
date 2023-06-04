using Gtk;
using Studio.Visual;

namespace Studio.Window;

public class MainWindow : Gtk.Window
{
    private ProjectExplorer explorer;

    public MainWindow() : base(WindowType.Toplevel)
    {
        SetDefaultSize(800, 600);
        Title = "Vision Studio";
        DeleteEvent += Window_DeleteEvent;

        explorer = new ProjectExplorer();
        LoadProjects();

        Box container = new Box(Orientation.Vertical, 0);
        Add(container);

        container.PackStart(explorer, true, true, 0);

        // Create the "Add Project" button
        Button addButton = new Button();
        addButton.Label = "Add Project";
        addButton.Clicked += OnAddButtonClicked;
        container.PackEnd(addButton, false, false, 0);
    }

    public void LoadProjects()
    {
        string[] projects = Core.ProjectManager.GetProjects();

        foreach (var project in projects)
        {
            Core.Project? nullable = Core.ProjectManager.LoadProject(project);
            if (!nullable.HasValue)
                continue;

            Core.Project p = nullable.Value;
            explorer.AddProject(p.Name, p);
        }
    }

    private void OnAddButtonClicked(object? sender, EventArgs e)
    {
        string path = PromptInput("Enter Project Path: ");
        Core.Project? project = Core.ProjectManager.LoadProject(path);

        if (!project.HasValue)
            return;

        explorer.AddProject(project.Value.Name, project.Value);
    }

    private string PromptInput(string message)
    {
        return "";
    }

    private void Window_DeleteEvent(object sender, DeleteEventArgs args)
    {
        Application.Quit();
    }
}