namespace Core;

public static class Init
{
    public static bool Initialize()
    {
        ProjectManager.InitProjectList();
        return true;
    }
}