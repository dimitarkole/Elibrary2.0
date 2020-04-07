using ELibrary.Data;
using ELibrary.Services.Data.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Services.Data.Tests.ClassFixtures
{
    public class InMemoryDatabaseFactory : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public InMemoryDatabaseFactory()
        {
            this.Context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
