namespace ELibrary.Services.Data.Tests
{   
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Common;
    using ELibrary.Data;
    using ELibrary.Services.Data.Tests.ClassFixtures;
    using ELibrary.Services.Data.Tests.Factories;
    using Xunit;

    public class TransientDbContextProvider : IClassFixture<MappingsProvider>
    {
        protected readonly ApplicationDbContext context;
        protected readonly string unitTestUserId;

        public TransientDbContextProvider()
        {
            this.unitTestUserId = GlobalConstants.UnitTestAdminId;
            context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }
    }
}
