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

    private static bool IsValidProject(string path)
    {
        return Directory.Exists(path) &&
               Directory.GetFiles(path)
                   .Any(x => x.EndsWith("manifest.json"));
    }

    public static void AddProject(string path)
    {
        string projectListPath = ProjectListPath();
        
        // Check if project already exists
        int count = IO.ReadFileLines(projectListPath)
            .Count(x => x == path);
        
        if(count > 0)
            return;
        
        // Check if project is valid
        if(!IsValidProject(path))
            return;
        
        IO.AppendFile(projectListPath, path + "\n");
    }

    public static string[] GetProjects()
    {
        string projectListPath = ProjectListPath();
        return IO.ReadFileLines(projectListPath)
            .Where(x => Directory.Exists(x))
            .ToArray();
    }

    public static void DeleteProject(string project)
    {
        List<string> lines = IO.ReadFileLines(ProjectListPath()).ToList();
        lines.Remove(project);
        
        IO.WriteFile(ProjectListPath(), string.Join("\n", lines));
    }
}