namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;
    private static Lazy<SingleInitializationSingleton> _instance = new(() => new SingleInitializationSingleton());

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
            _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton());
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

                    _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay));
                    _isInitialized = true;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
        
    }
}