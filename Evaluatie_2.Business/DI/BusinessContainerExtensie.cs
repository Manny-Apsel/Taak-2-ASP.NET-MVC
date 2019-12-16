using System;
using System.Collections.Generic;
using System.Text;
using Evaluatie_2.Business.Interfaces;
using Evaluatie_2.DataAccess.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Evaluatie_2.Business.DI
{
    public static class BusinessContainerExtensie
    {
        public static void ConfigureerBusiness(this IServiceCollection container)
        {
            container.ConfigureerDataAccess();
            container.AddTransient<IBureauLogica, BureauLogica>();
            container.AddTransient<IBureauLocatieLogica, BureauLocatieLogica>();
            container.AddTransient<IBureauTypeLogica, BureauTypeLogica>();
        }
    }
}