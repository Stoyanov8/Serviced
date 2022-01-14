namespace Serviced.Sample.Implementations
{
    using System;

    public class HouseService : ISingleton, IHasImplementationFactory
    {
        private readonly string _message;

        public HouseService()
        {
        }

        public HouseService(string message)
        {
            _message = message;
        }

        public void Log()
        {
            Console.WriteLine(_message);
        }

        public Func<IServiceProvider, object> GetFactory()
        {
            return x => new HouseService(nameof(HouseService) + " has been registered with implementation factory.");
        }
    }
}