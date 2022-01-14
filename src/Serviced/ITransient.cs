namespace Serviced
{
    /// <summary>
    /// Use this interface when you want to register service as transient
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransient : IServiced
    {
    }

    /// <summary>
    /// Use this interface when you want to register service as transient
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransient<T> : ITransient
    {
    }
}