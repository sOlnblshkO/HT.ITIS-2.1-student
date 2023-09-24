namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static Lazy<SingleInitializationSingleton> _lazy =
        new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(DefaultDelay), true);

    private static volatile bool _isInitialized = false;

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    public int Delay { get; }

    public static SingleInitializationSingleton Instance => _lazy.Value;

    internal static void Reset()
    {
        _lazy = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(DefaultDelay),true);
        _isInitialized = false;
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized) throw new InvalidOperationException("Cannot be initialized more than once!");
        _lazy = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay),true);
        _isInitialized = true;
    }
}