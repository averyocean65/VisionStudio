namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Core.Init.Initialize();
        Console.WriteLine("Welcome to Vision Studio!");
        
        // Testing
        Console.WriteLine("Input a project to add!");
        Console.Write("> ");
        
        
        string projectToAdd = Console.ReadLine();
        Core.ProjectManager.AddProject(projectToAdd);

        Core.Project? project = Core.ProjectManager.LoadProject(projectToAdd);
        if (project == null)
            return;
        
        Console.WriteLine("Project loaded!");
        
        Console.WriteLine($"Name: {project.Value.Name}");
        Console.WriteLine($"Version: {project.Value.Version}");
        Console.WriteLine($"Author: {project.Value.Author}");
        Console.WriteLine($"Path: {project.Value.Path}");
    }
}