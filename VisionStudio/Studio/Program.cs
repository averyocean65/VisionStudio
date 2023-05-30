namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Core.Init.Initialize();
        Console.WriteLine("Welcome to Vision Studio!");
        
        // Testing
        Core.Projects.AddProject("C:\\MyProject");
        Core.Projects.AddProject("C:\\MySecondProject");
    }
}