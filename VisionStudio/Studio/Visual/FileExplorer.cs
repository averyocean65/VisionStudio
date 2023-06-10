using Gtk;
using System.IO;

namespace Studio.Visual;

public class FileExplorer : ScrolledWindow
{
    private TreeView treeView;
    private ListStore listStore;

    private string baseDirectory;
    
    public FileExplorer(string directoryPath)
    {
        baseDirectory = directoryPath;
        
        treeView = new TreeView();
        treeView.HeadersVisible = false;
        treeView.EnableTreeLines = true;

        listStore = new ListStore(typeof(string), typeof(string));
        treeView.Model = listStore;

        TreeViewColumn nameColumn = new TreeViewColumn();
        CellRendererText nameRenderer = new CellRendererText();
        nameColumn.PackStart(nameRenderer, true);
        nameColumn.AddAttribute(nameRenderer, "text", 0);

        treeView.AppendColumn(nameColumn);

        PopulateTreeView(baseDirectory);
        treeView.RowActivated += (sender, args) =>
        {
            TreeIter iter;
            treeView.Model.GetIter(out iter, args.Path);
            string path = (string)treeView.Model.GetValue(iter, 1);

            if (File.Exists(path))
            {
                FileClicked?.Invoke(this, path);
            }
            else if (Directory.Exists(path))
            {
                ClearTreeView();
                PopulateTreeView(path);
                
                DirectoryClicked?.Invoke(this, path);
            }
        };

        Add(treeView);
    }
    
    public EventHandler<string> FileClicked;
    public EventHandler<string> DirectoryClicked;

    private void PopulateTreeView(string directoryPath)
    {
        if (directoryPath != baseDirectory)
        {
            string parentDirectory = Directory.GetParent(directoryPath).FullName;
            listStore.AppendValues("Go Back", parentDirectory);
        }

        string[] files = Directory.GetFiles(directoryPath);
        string[] directories = Directory.GetDirectories(directoryPath);

        foreach (string file in files)
        {
            string fileName = System.IO.Path.GetFileName(file);
            listStore.AppendValues(fileName, file);
        }

        foreach (string directory in directories)
        {
            string directoryName = System.IO.Path.GetFileName(directory);
            listStore.AppendValues(directoryName, directory);
        }
    }
    
    private void ClearTreeView()
    {
        listStore.Clear();
        treeView.Model = listStore;
    }
}