using Microsoft.Extensions.DependencyInjection;
using Serviced.Sample.ImplementationFactory;
using System;
using Test;

namespace Serviced.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();

            var serviceProvider = services
                  .AddServiced(AppDomain.CurrentDomain.GetAssemblies())
                  .BuildServiceProvider();

            var petService = serviceProvider.GetService<IPetService>();

            var houseService = serviceProvider .GetService<HouseService>();

            petService.Log();
            houseService.Log();
        }
    }
}