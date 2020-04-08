namespace ELibrary.Services.Data.Tests.ClassFixtures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Data.Tests.Factories;

    public class InMemoryDatabaseFactory : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public InMemoryDatabaseFactory()
        {
            this.Context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }
    }
}
