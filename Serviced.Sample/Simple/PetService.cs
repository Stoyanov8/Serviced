namespace Serviced.Sample
{
    using System;

    public class PetService : ITransient<IPetService>, IPetService
    {
        public void Log()
        {
            Console.WriteLine(nameof(PetService) + " has been registered.");
        }
    }
}