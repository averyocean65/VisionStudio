using Core;
using Gtk;
using Studio.Window;

namespace Studio.Visual;

public class ProjectExplorer : Box
{
    private ListStore listStore;
    private TreeView treeView;

    public MainWindow mainWindow;
    
    public ProjectExplorer(MainWindow window) : base(Orientation.Vertical, 0)
    {
        mainWindow = window;
        
        // Create the TreeView
        treeView = new TreeView();
        PackStart(treeView, true, true, 0);

        // Create the ListStore model
        listStore = new ListStore(typeof(string), typeof(Project));
        treeView.Model = listStore;

        // Add columns to the TreeView
        treeView.AppendColumn("Project", new CellRendererText(), "text", 0);

        // Handle selection changed event
        treeView.ButtonPressEvent += OnButtonPress;
        treeView.Selection.Changed += OnSelectionChanged;
    }

    public Project? selection { get; private set; }

    private Core.Project? GetProject()
    {
        if (treeView.Selection.GetSelected(out TreeIter iter))
        {
            string title = (string)listStore.GetValue(iter, 0);
            return (Project)listStore.GetValue(iter, 1);
        }

        return null;
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        selection = GetProject();
    }

    public void AddProject(Project project)
    {
        bool success = Core.ProjectManager.AddProject(project.Path);
        if (!success)
            return;

        DisplayProject(project);
    }

    public void DisplayProject(Project project)
    {
        listStore.AppendValues(project.Name, project);
        treeView.QueueDraw();
    }

    public void RemoveProject(string path)
    {
        TreeIter iter;

        for (bool valid = listStore.GetIterFirst(out iter); valid; valid = listStore.IterNext(ref iter))
        {
            var project = (Project)listStore.GetValue(iter, 1);
            if (project.Path == path)
            {
                listStore.Remove(ref iter);
                break;
            }
        }

        Core.ProjectManager.DeleteProject(path);

        treeView.QueueDraw();
    }

    private void OnButtonPress(object sender, EventArgs e)
    {
        Core.Project? project = GetProject();
        if (!project.HasValue)
            return;

        Console.WriteLine($"Name:\t\t {project.Value.Name}");
        Console.WriteLine($"Path:\t\t {project.Value.Path}");
        Console.WriteLine($"Author:\t\t {project.Value.Author}");
        Console.WriteLine($"Version:\t {project.Value.Version}");

        EditorWindow window = new EditorWindow();
        window.ShowAll();
        
        mainWindow.Hide();
    }
}