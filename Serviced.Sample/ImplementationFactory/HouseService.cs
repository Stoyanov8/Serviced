namespace Serviced.Sample.ImplementationFactory
{
    using System;

    public class HouseService : ISingleton<IHouseService>, IHouseService, IHasImplementationFactory
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
            return x => new HouseService("Registered with implementation factory");
        }
    }
}