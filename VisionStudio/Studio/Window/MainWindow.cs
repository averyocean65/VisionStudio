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

        Button removeButton = new Button();
        removeButton.Label = "Delete Project";
        removeButton.Clicked += OnRemoveButtonClicked;
        container.PackEnd(removeButton, false, false, 0);

        Button addButton = new Button();
        addButton.Label = "Add Project";
        addButton.Clicked += OnAddButtonClicked;
        container.PackEnd(addButton, false, false, 0);
    }

    private void OnRemoveButtonClicked(object? sender, EventArgs e)
    {
        if (!explorer.selection.HasValue)
            return;

        explorer.RemoveProject(explorer.selection.Value.Path);
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
            explorer.DisplayProject(p);
        }
    }

    private async void OnAddButtonClicked(object? sender, EventArgs e)
    {
        string path = await PromptInput("Input Path!", "Enter Project Path: ");
        Core.Project? project = Core.ProjectManager.LoadProject(path);

        if (!project.HasValue)
            return;

        explorer.AddProject(project.Value);
    }

    private Task<string> PromptInput(string title, string message)
    {
        var tcs = new TaskCompletionSource<string>();

        Dialog dialog = new Dialog();
        dialog.SetDefaultSize(500, 75);

        dialog.Title = title;
        dialog.Modal = true;
        dialog.Resizable = false;

        Entry entry = new Entry();
        entry.WidthChars = 20;
        dialog.ContentArea.PackStart(entry, false, false, 0);

        Button ok = new Button("OK");
        ok.Clicked += (sender, args) =>
        {
            string userInput = entry.Text;
            dialog.Destroy();
            tcs.SetResult(userInput);
        };

        dialog.ActionArea.PackStart(ok, false, false, 0);
        dialog.ShowAll();

        return tcs.Task;
    }

    private void Window_DeleteEvent(object sender, DeleteEventArgs args)
    {
        Application.Quit();
    }
}