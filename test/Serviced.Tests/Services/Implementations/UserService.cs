namespace Serviced.Tests.Services.Implementations
{
    using Serviced.Tests.Services.Interfaces;

    using static TestConstants;

    internal class UserService : ITransient<IUserService>, IUserService
    {
        public string GetName() => NAME;
    }
}
