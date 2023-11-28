using System.Diagnostics;
using System.Runtime.InteropServices;
using Hw3.Mutex;
using Tests.RunLogic.Attributes;
using Xunit.Abstractions;

namespace Tests.CSharp.Homework3;

public class ConcurrencyTests
{
    private readonly ITestOutputHelper _toh;

    public ConcurrencyTests(ITestOutputHelper toh)
    {
        _toh = toh;
    }

    [Homework(Homeworks.HomeWork3)]
    public void SingleThread_NoRaces()
    {
        var expected = Concurrency.Increment(1, 1000);
        Assert.Equal(expected, Concurrency.Index);
    }

    [Homework(Homeworks.HomeWork3)]
    public void FiveThreads_100Iterations_RaceIsHardToReproduce()
    {
        var expected = Concurrency.Increment(2, 1000);
        Assert.Equal(expected, Concurrency.Index);
    }

    [Homework(Homeworks.HomeWork3)]
    public void EightThreads_100KIterations_RaceIsReproduced()
    {
        var expected = Concurrency.Increment(2, 100_000);
        Assert.NotEqual(expected, Concurrency.Index);
        _toh.WriteLine($"Expected: {expected}; Actual: {Concurrency.Index}");
    }

    [Homework(Homeworks.HomeWork3)]
    public void EightThreads_100KIterations_WithLock_NoRaces()
    {
        var expected = Concurrency.IncrementWithLock(2, 100_000);
        Assert.Equal(expected, Concurrency.Index);
        _toh.WriteLine($"Expected: {expected}; Actual: {Concurrency.Index}");
    }

    [Homework(Homeworks.HomeWork3)]
    public void EightThreads_100KIterations_LockIsSyntaxSugarForMonitor_NoRaces()
    {
        var expected = Concurrency.IncrementWithLock(2, 100_000);
        Assert.Equal(expected, Concurrency.Index);
        _toh.WriteLine($"Expected: {expected}; Actual: {Concurrency.Index}");
    }

    [Homework(Homeworks.HomeWork3)]
    public void EightThreads_100KIterations_WithInterlocked_NoRaces()
    {
        var expected = Concurrency.IncrementWithInterlocked(2, 100_000);
        Assert.Equal(expected, Concurrency.Index);
        _toh.WriteLine($"Expected: {expected}; Actual: {Concurrency.Index}");
    }

    [Homework(Homeworks.HomeWork3)]
    public void EightThreads_100KIterations_InterlockedIsFasterThanLock_Or_IsIt()
    {
        var isM1Mac =
            OperatingSystem.IsMacOS()
            && RuntimeInformation.ProcessArchitecture == Architecture.Arm64;

        var elapsedWithLock = StopWatcher.Stopwatch(EightThreads_100KIterations_WithLock_NoRaces);
        var elapsedWithInterlocked = StopWatcher.Stopwatch(
            EightThreads_100KIterations_WithInterlocked_NoRaces
        );

        _toh.WriteLine($"Lock: {elapsedWithLock}; Interlocked: {elapsedWithInterlocked}");

        // see: https://godbolt.org/z/1TzWMz4aj
        if (isM1Mac)
            Assert.True(elapsedWithLock < elapsedWithInterlocked);
        else
            Assert.True(elapsedWithLock > elapsedWithInterlocked);
    }

    [Homework(Homeworks.HomeWork3)]
    public void Semaphore()
    {
        var delay = 3_000;
        var semaphore = new Semaphore(2, 2);
        var testFunction = () =>
        {
            _toh.WriteLine(
                $"{DateTime.Now.ToString("HH:mm:ss")}: Process {Process.GetCurrentProcess().Id} waits the semaphore"
            );
            if (semaphore.WaitOne(delay + 100))
            {
                _toh.WriteLine(
                    $"{DateTime.Now.ToString("HH:mm:ss")}: Process {Process.GetCurrentProcess().Id} starts"
                );
                Thread.Sleep(delay);
                semaphore.Release();
            }
            else
            {
                _toh.WriteLine(
                    $"{DateTime.Now.ToString("HH:mm:ss")}: Process {Process.GetCurrentProcess().Id} waiting timeout"
                );
            }

            _toh.WriteLine(
                $"{DateTime.Now.ToString("HH:mm:ss")}: Process {Process.GetCurrentProcess().Id} ends"
            );
        };

        var processes = new[]
        {
            new Task(testFunction),
            new Task(testFunction),
            new Task(testFunction)
        };

        var sw = new Stopwatch();
        sw.Start();

        foreach (var process in processes)
            process.Start();
        Task.WaitAll(processes);

        Assert.True(sw.ElapsedMilliseconds >= delay * 2);
        Assert.False(sw.ElapsedMilliseconds >= delay * 3);
    }

    [Homework(Homeworks.HomeWork3)]
    public async Task SemaphoreSlimWithTasks()
    {
        var expected = await Concurrency.IncrementAsync(8, 100_000);
        Assert.Equal(expected, Concurrency.Index);
    }

    // public void NamedSemaphore_InterprocessCommunication()
    // {
    // My platform does not support Named Semaphores, so
    // I cannot do this task. See:
    // https://github.com/dotnet/runtime/issues/4370#issuecomment-692977704
    // }

    [Homework(Homeworks.HomeWork3)]
    public void ConcurrentDictionary_100KIterations_WithInterlocked_NoRaces()
    {
        var expected = Concurrency.IncrementWithConcurrentDictionary(8, 100_000);
        Assert.Equal(expected, Concurrency.Index);
    }

    [Homework(Homeworks.HomeWork3)]
    public async Task Mutex()
    {
        var p1 = new Process { StartInfo = GetProcessStartInfo() };
        var p2 = new Process { StartInfo = GetProcessStartInfo() };

        var sw = new Stopwatch();
        sw.Start();
        p1.Start();
        p2.Start();
        await p1.WaitForExitAsync();
        await p2.WaitForExitAsync();
        p1.WaitForExit();
        var val = await p1.StandardOutput.ReadToEndAsync();
        _toh.WriteLine(val);

        p2.WaitForExit();
        val = await p2.StandardOutput.ReadToEndAsync();
        sw.Stop();
        _toh.WriteLine(val);

        Assert.True(sw.Elapsed.TotalMilliseconds >= WithMutex.Delay * 2);
    }

    private static ProcessStartInfo GetProcessStartInfo()
    {
        return new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run --project ../../../../Homework3/Hw3.Mutex/Hw3.Mutex.csproj",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
    }
}