using Core;
using Gtk;

namespace Studio.Visual;

public class ProjectExplorer : Box
{
    private ListStore listStore;
    private TreeView treeView;

    public ProjectExplorer() : base(Orientation.Vertical, 0)
    {
        // Create the TreeView
        treeView = new TreeView();
        PackStart(treeView, true, true, 0);

        // Create the ListStore model
        listStore = new ListStore(typeof(string), typeof(Project));
        treeView.Model = listStore;

        // Add columns to the TreeView
        treeView.AppendColumn("Project", new CellRendererText(), "text", 0);

        // Handle selection changed event
        treeView.ButtonPressEvent += OnSelectionChanged;
    }

    public void AddProject(string title, Project project)
    {
        listStore.AppendValues(title, project);
    }

    private void OnSelectionChanged(object sender, EventArgs e)
    {
        if (treeView.Selection.GetSelected(out TreeIter iter))
        {
            string title = (string)listStore.GetValue(iter, 0);
            Project project = (Project)listStore.GetValue(iter, 1);
            Console.WriteLine("Selected Project: " + project.Name);
            Console.WriteLine("Project Path: " + project.Path);
            Console.WriteLine("Project Author: " + project.Author);
        }
    }
}