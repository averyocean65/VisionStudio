using Gtk;
using Studio.Visual;

namespace Studio.Window;

public class EditorWindow : Gtk.Window
{
    private Paned mainPane;
    private FileExplorer fileExplorer;
    private MultilineTextInput textEditor;
    private Entry textField;

    private string _projectPath;

    public EditorWindow(string project) : base(Gtk.WindowType.Toplevel)
    {
        SetDefaultSize(800, 600);
        Maximize();

        _projectPath = project;
        
        DeleteEvent += delegate { Application.Quit(); };

        mainPane = new Paned(Orientation.Horizontal);
        Add(mainPane);

        fileExplorer = new FileExplorer(_projectPath);
        textField = new Entry();

        var scrollableEditor = new ScrolledWindow();

        textEditor = new MultilineTextInput();
        scrollableEditor.Add(textEditor);

        mainPane.Add(fileExplorer);
        mainPane.Add(scrollableEditor);
        mainPane.Position = 200;

        fileExplorer.FileClicked += (sender, args) =>
        {
            textEditor.Text = Core.IO.ReadFile(args);
            textEditor.SetLanguage(args);
            textField.Text = args;
        };
        
        var vbox = new VBox(false, 0);
        vbox.PackStart(textField, false, false, 0);
        vbox.PackStart(mainPane, true, true, 0);

        Add(vbox);
    }
}