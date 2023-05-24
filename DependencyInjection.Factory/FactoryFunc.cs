namespace DependencyInjection.Factory;

public class FactoryFunc<T> : IDisposable, IFactory<T> where T : class
{
    private Func<T> _func;
    private bool _disposedValue;

    public FactoryFunc(Func<T> func)
    {
        _func = func;
    }
    public T Create()
    {
        T service = _func() ?? throw new InvalidOperationException();
        return service;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _func = null!;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Factory()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
