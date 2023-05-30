namespace Core;

public static class IO
{
    public static void CreateIfDoesntExist(string path)
    {
        if (File.Exists(path))
            return;
        
        File.WriteAllText(path, "");
    }
}