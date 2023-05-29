using System.Runtime.InteropServices;

namespace VisionStudio;

public class Kernel
{
    [DllImport("libVisionCore", EntryPoint = "test_lib")]
    public static extern int TestLib();
}