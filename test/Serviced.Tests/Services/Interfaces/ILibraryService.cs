namespace Serviced.Tests.Services.Interfaces
{
    using System.Collections.Generic;

    internal interface ILibraryService
    {
        IEnumerable<string> GetBooks();
    }
}
