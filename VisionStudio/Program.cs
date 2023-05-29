namespace VisionStudio;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("DLL Test: ");

        int testLib = Kernel.TestLib();
        Console.WriteLine(testLib.ToString());
    }
}