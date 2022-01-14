using System;
using System.Collections.Generic;

namespace Serviced.Tests.Services.Interfaces
{
    internal class LibraryService : ISingleton, IHasImplementationFactory
    {
        private readonly ICollection<string> _books;

        public LibraryService()
        {
            this._books = new List<string>();
        }

        public IEnumerable<string> GetBooks() => this._books;

        public Func<IServiceProvider, object> GetFactory()
        {
            var libraryService = new LibraryService();
            libraryService._books.Add("The First Law");

            return x => libraryService;
        }
    }
}
