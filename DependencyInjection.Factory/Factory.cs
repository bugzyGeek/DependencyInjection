namespace DependencyInjection.Factory;

/// <summary>
/// A factory used to create a service type registered to the specified <typeparamref name="IServiceCollection"/> the <typeparamref name="Factory"/> is initialized
/// </summary>
/// <typeparam name="T">The service type to be created</typeparam>
public class Factory<T> : IDisposable, IFactory<T> where T : class
{
    private bool _disposedValue;
    private T _interface;
    public Factory(T @interface)
    {
        _interface = @interface;
    }

    /// <summary>
    /// Create an instance of a service type of <typeparamref name="T"/> 
    /// </summary>
    /// <returns>Returns an instance of <typeparamref name="T"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual T Create()
    {
        T service = _interface ?? throw new InvalidOperationException();
        return service;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _interface = null!;
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
