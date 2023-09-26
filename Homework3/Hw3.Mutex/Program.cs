using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Hw3.Mutex;
[ExcludeFromCodeCoverage]

class Program
{
    public static void Main()
    {
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Process.GetCurrentProcess().Id} starts");
        using var wm = new WithMutex();
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Process.GetCurrentProcess().Id} acquires mutex");
        Console.WriteLine(wm.Increment());
        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {Process.GetCurrentProcess().Id} releases mutex");
    }
}

