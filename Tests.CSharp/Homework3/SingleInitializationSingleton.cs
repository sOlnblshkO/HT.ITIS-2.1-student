namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;
    private static SingleInitializationSingleton? _instance;

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    public int Delay { get; }

    public static SingleInitializationSingleton Instance
    {
        get
        {
            if (_instance==null)
            {
                lock (Locker)
                {
                    if (_instance == null)
                    {
                        _instance = new SingleInitializationSingleton();
                        _isInitialized = true;
                    }
                }
            }
            return _instance;
        }
    }

    internal static void Reset()
    {
        lock (Locker)
        {
            _instance = null;
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {   
        lock (Locker)
        {
            if (_instance != null)
                throw new InvalidOperationException("The instance has already been initialized");

            _instance = new SingleInitializationSingleton(delay);
            _isInitialized = true;
        }
    }
}