namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static Lazy<SingleInitializationSingleton> _lazy = new (()=>new SingleInitializationSingleton());

    private static volatile bool _isInitialized;

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
        _lazy = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(), true);
        _isInitialized = false;
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized) throw new InvalidOperationException();
        _lazy = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay));
        _isInitialized = !_isInitialized;
    }
    
}
