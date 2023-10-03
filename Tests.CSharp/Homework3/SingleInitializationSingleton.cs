namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    private static Lazy<SingleInitializationSingleton> _instance = new();

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    public int Delay { get; }

    public static SingleInitializationSingleton Instance => _instance.Value;


    internal static void Reset()
    {
        lock (Locker)
        {
            _instance = new(() => new SingleInitializationSingleton(DefaultDelay), true);
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {
        if (!_isInitialized)
        {
            lock (Locker)
            {
                if (!_isInitialized)
                {
                    _instance = new(() => new SingleInitializationSingleton(delay), true);
                    _isInitialized = true;
                    return;
                }
            }
        }
        throw new InvalidOperationException("Singlton was already initialized");
    }
}