namespace Core;

public static class IO
{
    public static void CreateIfDoesntExist(string path)
    {
        if (File.Exists(path))
            return;

        File.WriteAllText(path, "");
    }

    public static void WriteFile(string path, string content)
        => File.WriteAllText(path, content);

    public static void WriteFile(string path, byte[] content)
        => File.WriteAllBytes(path, content);

    public static void AppendFile(string path, string content)
    {
        string file = ReadFile(path);
        WriteFile(path, file + content);
    }

    public static void AppendFile(string path, byte[] content)
    {
        byte[] file = ReadFileBytes(path);
        WriteFile(path, file.Concat(content).ToArray());
    }
    
    public static string ReadFile(string path)
        => File.ReadAllText(path);
    
    public static byte[] ReadFileBytes(string path)
        => File.ReadAllBytes(path);
    
    public static string[] ReadFileLines(string path)
        => File.ReadAllLines(path);
    
    public static void DeleteFile(string path)
        => File.Delete(path);
    
    public static bool IsDirectory(string path)
        => Directory.Exists(path);
    
    public static void CreateDirectory(string path)
        => Directory.CreateDirectory(path);
    
    public static void DeleteDirectory(string path)
        => Directory.Delete(path);
}