using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework3;

public class SingleInitializationSingletonTests
{
    [Homework(Homeworks.HomeWork3)]
    public void DefaultInitialization_ReturnsSingleInstance()
    {
        SingleInitializationSingleton.Reset();
        SingleInitializationSingleton? i1 = null;
        var elapsed = StopWatcher.Stopwatch(() =>
        {
            i1 = SingleInitializationSingleton.Instance;
        });

        var i2 = SingleInitializationSingleton.Instance;
        Assert.Equal(i2, i1);

        Assert.True(elapsed.TotalMilliseconds >= i2.Delay);
    }

    [Homework(Homeworks.HomeWork3)]
    public void CustomInitialization_ReturnsSingleInstance()
    {
        SingleInitializationSingleton.Reset();
        var delay = 5_000;
        SingleInitializationSingleton.Initialize(delay);
        var elapsed = StopWatcher.Stopwatch(() =>
        {
            var i = SingleInitializationSingleton.Instance;
            Assert.Equal(i, SingleInitializationSingleton.Instance);
        });

        Assert.True(elapsed.TotalMilliseconds > SingleInitializationSingleton.DefaultDelay);
        Assert.True(elapsed.TotalMilliseconds >= delay);
    }

    [Homework(Homeworks.HomeWork3)]
    public void DoubleInitializationAttemptThrowsException()
    {
        SingleInitializationSingleton.Initialize(2);
        Assert.Throws<InvalidOperationException>(() =>
        {
            SingleInitializationSingleton.Initialize(3);
        });
    }

    [Homework(Homeworks.HomeWork3)]
    public void ConcurrentExecution_DoesNotThrowException()
    {
        SingleInitializationSingleton.Initialize(1);
        var thread1 = new Thread(() =>
        {
            SingleInitializationSingleton.Reset();
            Thread.Sleep(1);
            SingleInitializationSingleton.Reset();
            Thread.Sleep(1);
            SingleInitializationSingleton.Reset();
        });
        var thread2 = new Thread(() =>
        {
            Thread.Sleep(1);
            SingleInitializationSingleton.Initialize(100);
            Thread.Sleep(1);
            SingleInitializationSingleton.Initialize(100);
            Thread.Sleep(1);
            SingleInitializationSingleton.Initialize(100);
        });
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
        Assert.Equal(100, SingleInitializationSingleton.Instance.Delay);
    }
}
