using Gtk;
using Studio.Window;

namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Initializing Core...");
        
        Core.Init.Initialize();
        Application.Init();

        // Settings.Default.ApplicationPreferDarkTheme = true;

        Console.WriteLine("Core Initialized!");
        
        MainWindow window = new MainWindow();
        window.ShowAll();

        Application.Run();
    }
}