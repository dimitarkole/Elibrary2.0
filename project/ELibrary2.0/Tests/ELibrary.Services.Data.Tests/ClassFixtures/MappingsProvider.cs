
using ELibrary.Services.Mapping;
using ELibrary.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ELibrary.Services.Data.Tests.ClassFixtures
{
    public class MappingsProvider
    {
        public MappingsProvider()
        {
            //Register all mappings in the app
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
