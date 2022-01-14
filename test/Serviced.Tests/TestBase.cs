namespace Serviced.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public abstract class TestBase
    {
        public TestBase()
        {
            var services = new ServiceCollection();

            this.ServiceProvider = services
                  .AddServicedForCallingAssembly()
                  .BuildServiceProvider();

            this.Services = services;
        }

        protected IServiceCollection Services { get; set; }

        protected IServiceProvider ServiceProvider { get; set; }

        protected TService GetService<TService>() 
            => this.ServiceProvider.GetService<TService>();

        protected ServiceDescriptor GetServiceDescription<TService>() 
            => this.Services.FirstOrDefault(x => x.ServiceType == typeof(TService));
    }
}
