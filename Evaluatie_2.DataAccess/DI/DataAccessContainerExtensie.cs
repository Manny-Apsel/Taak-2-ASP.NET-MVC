using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Evaluatie_2.DataAccess.DI
{
    public static class DataAccessContainerExtensie
    {
        public static void ConfigureerDataAccess(this IServiceCollection container)
        {
            container.AddDbContext<DatabaseContext>();
        }
    }
}