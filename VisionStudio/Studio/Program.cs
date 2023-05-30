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
        Core.Projects.AddProject(projectToAdd);
        
        Console.WriteLine("Test Deleting Projects...");
        
        Console.WriteLine("Current Projects:");
        foreach(string project in Core.Projects.GetProjects())
            Console.WriteLine(project);
        
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        
        Core.Projects.DeleteProject("C:\\MyProject");
    }
}