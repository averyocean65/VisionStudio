using Gtk;
using Studio.Window;

namespace Studio;

public class Program
{
    public static void Main(string[] args)
    {
        Core.Init.Initialize();
        Application.Init();

        MainWindow window = new MainWindow();
        window.ShowAll();

        Application.Run();
    }
}