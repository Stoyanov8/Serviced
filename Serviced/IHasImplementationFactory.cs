namespace Serviced
{
    using System;

    /// <summary>
    /// Use this interface when you have different registration strategy, for example, you want to register concrete instance of an object
    /// NOTE: Provide parameterless constructor for your class
    /// </summary>
    public interface IHasImplementationFactory
    {
        /// <summary>
        /// Implementation factory
        /// </summary>
        /// <returns></returns>
        Func<IServiceProvider, object> GetFactory();
    }
}