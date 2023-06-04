using Gtk;

namespace Studio.Window;

public class EditorWindow : Gtk.Window
{
    private Paned mainPane;
    private TreeView fileExplorer;
    private Notebook textEditor;
    private Entry textField;

    public EditorWindow() : base(Gtk.WindowType.Toplevel)
    {
        SetDefaultSize(800, 600);
        Maximize();

        DeleteEvent += delegate { Application.Quit(); };

        mainPane = new Paned(Orientation.Horizontal);
        Add(mainPane);

        fileExplorer = new TreeView();
        // Add columns and populate the file explorer tree view here

        textField = new Entry();

        var scrollableEditor = new ScrolledWindow();
        scrollableEditor.Add(textEditor);

        mainPane.Add1(fileExplorer);
        mainPane.Add2(scrollableEditor);
        mainPane.Position = 200;

        var vbox = new VBox(false, 0);
        vbox.PackStart(textField, false, false, 0);
        vbox.PackStart(mainPane, true, true, 0);

        Add(vbox);
    }
}