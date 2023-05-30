namespace Core;

public static class Projects
{
    private static string ProjectListPath()
    {
        return Path.Combine(Environment.CurrentDirectory, "projects.txt");
    }
    
    public static void InitProjectList()
    {
        string projectListPath = ProjectListPath();
        IO.CreateIfDoesntExist(projectListPath);
    }
}