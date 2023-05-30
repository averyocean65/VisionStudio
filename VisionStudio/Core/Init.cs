namespace Core;

public static class Init
{
    public static bool Initialize()
    {
        Projects.InitProjectList();
        return true;
    }
}