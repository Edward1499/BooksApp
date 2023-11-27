using Core.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBookRepository, BookRepository>();

            return serviceCollection;
        }
    }
}
