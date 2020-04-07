using ELibrary.Data;
using ELibrary.Services.Data.Tests.ClassFixtures;
using ELibrary.Services.Data.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ELibrary.Services.Data.Tests
{   
    public class TransientDbContextProvider : IClassFixture<MappingsProvider>
    {
        protected readonly ApplicationDbContext context;

        public TransientDbContextProvider()
        {
            context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }
    }
}
