namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    private static Lazy<SingleInitializationSingleton> _lazy =
        new(() => new SingleInitializationSingleton(), true);
    
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
        lock (Locker)
        {
            if (!_isInitialized) return;
            
            _lazy = new(() => new SingleInitializationSingleton(), true);
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {
        lock (Locker)
        {
            if (_isInitialized)
                throw new InvalidOperationException("SingleInitializationSingleton already initialized");
            
            _lazy = new(() => new SingleInitializationSingleton(delay), true);
            _isInitialized = true;
        }
    }
}