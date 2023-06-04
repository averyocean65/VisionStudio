using Gtk;
using Studio.Window;

namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Core.Init.Initialize();
        // MainMenu();

        Application.Init();

        MainWindow window = new MainWindow();
        window.ShowAll();

        Application.Run();
    }
}