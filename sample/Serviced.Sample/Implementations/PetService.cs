namespace Serviced.Sample.Implementations
{
    using Serviced;
    using Serviced.Sample.Interfaces;
    using System;

    public class PetService : ITransient<IPetService>, IPetService
    {
        public void Log()
        {
            Console.WriteLine(nameof(PetService) + " has been registered as transient.");
        }
    }
}