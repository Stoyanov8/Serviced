namespace Serviced.Tests.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Serviced.Tests.Services.Interfaces;
    using Xunit;

    public class ImplementationFactoryTests : TestBase
    {  
        [Fact]
        public void LibraryService_ShouldNotBeNull()
        {
            var libraryService = ServiceProvider.GetService<LibraryService>();

            Assert.NotNull(libraryService);
        }

        [Fact]
        public void LibraryService_IsRegisteredAsSingleton()
        {
            var descriptor = GetServiceDescription<LibraryService>();

            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        }

        [Fact]
        public void LibraryService_IsRegisteredAsSelf()
        {
            var descriptor = GetServiceDescription<LibraryService>();

            Assert.Equal(typeof(LibraryService), descriptor.ServiceType);
        }

        [Fact]
        public void LibraryService_HasImplementationFactory()
        {
            var descriptor = GetServiceDescription<LibraryService>();

            Assert.NotNull(descriptor.ImplementationFactory);
        }


        [Fact]
        public void LibraryService_HasBooks()
        {
            var libraryService = ServiceProvider.GetService<LibraryService>();

            var books = libraryService.GetBooks();

            Assert.Single(books);
        }
    }
}
