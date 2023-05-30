using Newtonsoft.Json;

namespace Core;

public struct Project
{
    /* JSON Values */
    public string Name;
    public string Version;
    public string Author;

    /* Runtime Values */
    public string Path;
}

public static class ProjectManager
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

    public static Project? LoadProject(string path)
    {
        if (!IsValidProject(path))
            return null;
        
        // Get Manifest Path
        string manifestPath = Path.Combine(path, "manifest.json");
        
        // Read Manifest
        string manifest = IO.ReadFile(manifestPath);
        
        // Parse Manifest
        JsonReader reader = new JsonTextReader(new StringReader(manifest));
        JsonSerializer serializer = JsonSerializer.Create();
        Project project = serializer.Deserialize<Project>(reader);
        
        // Set Project Path
        project.Path = path;
        return project;
    }

    public static void DeleteProject(string project)
    {
        List<string> lines = IO.ReadFileLines(ProjectListPath()).ToList();
        lines.Remove(project);
        
        IO.WriteFile(ProjectListPath(), string.Join("\n", lines));
    }
}