namespace Serviced
{
    /// <summary>
    /// Use this interface when you want to register service as singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton : IServiced
    {
    }

    /// <summary>
    /// Use this interface when you want to register service as singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton<T> : ISingleton
    {
    }
}