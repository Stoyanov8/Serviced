namespace Serviced.Tests.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Serviced.Tests.Services.Interfaces;
    using Xunit;

    public class TransientTests : TestBase
    {
        [Fact]
        public void UserService_ShouldNotBeNull()
        {
            var userService = ServiceProvider.GetService<IUserService>();

            Assert.NotNull(userService);
        }

        [Fact]
        public void UserService_IsRegisteredAsTransient()
        {
            var descriptor = GetServiceDescription<IUserService>();

            Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
        }

        [Fact]
        public void UserService_IsRegisteredFromIUserService()
        {
            var descriptor = GetServiceDescription<IUserService>();

            Assert.Equal(typeof(IUserService), descriptor.ServiceType);
        }

        [Fact]
        public void User_IsEminem()
        {
            var userService = ServiceProvider.GetService<IUserService>();

            Assert.Equal("My name is Chika-chika Slim Shady", userService.GetName());
        }
    }
}
