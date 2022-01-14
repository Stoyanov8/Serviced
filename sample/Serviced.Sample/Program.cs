namespace Serviced.Sample
{
    using Microsoft.Extensions.DependencyInjection;
    using Serviced.Sample.Implementations;
    using Serviced.Sample.Interfaces;

    internal class Program
    {
        private static void Main()
        {
            var services = new ServiceCollection();
            var serviceProvider = services
                  .AddServicedForCallingAssembly()
                  .BuildServiceProvider();

            var petService = serviceProvider.GetService<IPetService>();

            var houseService = serviceProvider.GetService<HouseService>();

            petService.Log();
            houseService.Log();
        }
    }
}