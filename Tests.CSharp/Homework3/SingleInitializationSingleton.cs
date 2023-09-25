namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();
    private static SingleInitializationSingleton _instance;
    private static volatile bool _isInitialized = false;

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
            if (_isInitialized)
            {
                Thread.Sleep(_instance.Delay);
                return _instance;
            }
            _instance = new SingleInitializationSingleton();
            _isInitialized = true;
            Thread.Sleep(_instance.Delay);
            return _instance;
        }
    }

    internal static void Reset()
    {
        _instance = null;
        _isInitialized = false;
    }

    public static void Initialize(int delay)
    {
        try
        {
            if (!_isInitialized)
            {
                lock (Locker)
                {
                    if (!_isInitialized)
                    {
                        _instance = new SingleInitializationSingleton(delay);
                        _isInitialized = true;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Already was initialized");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}