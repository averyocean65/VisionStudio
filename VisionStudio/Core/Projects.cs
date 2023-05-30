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

    public static void AddProject(string path)
    {
        string projectListPath = ProjectListPath();
        
        // Check if project already exists
        int count = IO.ReadFileLines(projectListPath)
            .Count(x => x == path);
        
        if(count > 0)
            return;

        IO.AppendFile(projectListPath, path + "\n");
    }

    public static string[] GetProjects()
    {
        string projectListPath = ProjectListPath();
        return IO.ReadFileLines(projectListPath);
    }

    public static void DeleteProject(string project)
    {
        List<string> lines = IO.ReadFileLines(ProjectListPath()).ToList();
        lines.Remove(project);
        
        IO.WriteFile(ProjectListPath(), string.Join("\n", lines));
    }
}