namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();
    private static volatile Lazy<SingleInitializationSingleton> _instance = new(() =>
        new SingleInitializationSingleton());
    private static volatile bool _isInitialized = false;

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
        if (!_isInitialized) return;
        
        lock (Locker)
        {
            if (!_isInitialized) return;
            
            _instance = new Lazy<SingleInitializationSingleton>();
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized)
            throw new InvalidOperationException("Already was initialized");

        lock (Locker)
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already was initialized");

            _instance = new Lazy<SingleInitializationSingleton>(() =>
                new SingleInitializationSingleton(delay));
            _isInitialized = true;
        }
    }
}