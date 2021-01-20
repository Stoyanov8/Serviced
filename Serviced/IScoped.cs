namespace Serviced
{
    /// <summary>
    /// Use this interface when you want to register service as scoped
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IScoped : IServiced
    {
    }

    /// <summary>
    /// Use this interface when you want to register service as scoped
    /// </summary>
    /// <typeparam name="T"></typeparam
    public interface IScoped<T> : IScoped
    {
    }
}