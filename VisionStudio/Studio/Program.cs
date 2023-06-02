using Gtk;

namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Core.Init.Initialize();
        // MainMenu();

        Application.Init();

        Window window = new Window("Vision Studio");
        window.SetDefaultSize(250, 100);
        window.DeleteEvent += (sender, args) => { Application.Quit(); };

        // Create a vertical box container
        Box box = new Box(Orientation.Vertical, 10);
        window.Add(box);

        // Create a label
        Label label = new Label("Welcome to Vision Studio!");

        // Create a button
        Button button = new Button("Click me!");
        button.Clicked += (sender, eventArgs) => { Console.WriteLine("Hello!"); };

        // Add the label and button to the box
        box.PackStart(label, true, true, 0);
        box.PackStart(button, false, false, 0);

        // Show all the widgets
        window.ShowAll();

        Application.Run();
    }

    public static void MainMenu()
    {
        Console.Clear();

        Console.WriteLine("Welcome to Vision Studio!");
        Console.WriteLine("Please select an option:");

        Console.WriteLine("1. Register Project");
        Console.WriteLine("2. Open Project");
        Console.WriteLine("3. Delete Project");
        Console.WriteLine("4. Exit");

        Console.Write("> ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                RegisterProject();
                break;
            case "2":
                OpenProject();
                break;
            case "3":
                DeleteProject();
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option!");
                MainMenu();
                return;
        }
    }

    private static void DeleteProject()
    {
        Console.Clear();

        Core.Project? project = GetProject();

        if (project == null)
        {
            Error("Failed to load project!");
            return;
        }

        Console.WriteLine($"Are you sure you want to delete {project.Value.Name}? (y/n)");
        Console.Write("> ");

        char input = Console.ReadLine().ToLower()[0];

        if (input == 'y')
        {
            Core.ProjectManager.DeleteProject(project.Value.Path);
            Console.WriteLine("Project deleted!");
        }
        else
        {
            Console.WriteLine("Project not deleted!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        MainMenu();
    }

    private static void Error(string msg)
    {
        Console.WriteLine(msg);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        MainMenu();
    }

    private static Core.Project? GetProject()
    {
        Console.WriteLine("Please select a project:");
        string[] projects = Core.ProjectManager.GetProjects();

        for (int i = 0; i < projects.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {projects[i]}");
        }

        Console.Write("> ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int index))
        {
            Error("Invalid option!");
            return null;
        }

        if (index < 1 || index > projects.Length)
        {
            Error("Invalid option!");
            return null;
        }

        string projectPath = projects[index - 1];
        Core.Project? project = Core.ProjectManager.LoadProject(projectPath);

        return project;
    }

    private static void OpenProject()
    {
        Console.Clear();

        Core.Project? project = GetProject();

        if (project == null)
        {
            Error("Failed to load project!");
            return;
        }

        // Show Project Information
        Console.WriteLine();
        Console.WriteLine($"Name:       {project.Value.Name}");
        Console.WriteLine($"Version:    {project.Value.Version}");
        Console.WriteLine($"Author(s):  {project.Value.Author}");
        Console.WriteLine($"Path:       {project.Value.Path}");
        Console.WriteLine();

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        MainMenu();
    }

    private static void RegisterProject()
    {
        Console.Clear();

        Console.WriteLine("Please enter the path to the project:");
        Console.Write("> ");

        string path = Console.ReadLine();
        bool success = Core.ProjectManager.AddProject(path);

        if (success)
            Console.WriteLine("Project registered successfully!");
        else
            Console.WriteLine("Project registration failed!");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        MainMenu();
    }
}