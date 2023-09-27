namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();
    private static Lazy<SingleInitializationSingleton> _instance = new(() => new SingleInitializationSingleton(), true);

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
        lock (Locker)
        {
            _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(DefaultDelay), true);
            _isInitialized = false;
        }
        
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized)
            throw new InvalidOperationException("Cannot be initialized multiple times");
        
        lock (Locker)
        {
            if(_isInitialized)
                throw new InvalidOperationException("Cannot be initialized multiple times");
                
            _instance = new(() => new SingleInitializationSingleton(delay), true);
            _isInitialized = true;   
            
            
        }
    }
}