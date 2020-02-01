using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShapeCalculations.Application.Contracts;
using ShapeCalculations.Application.Impl;
using ShapeCalculations.Entity.Intersector;

namespace ShapeCalculations.WebApi.App_Start
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IParserService, ParserService>();
            services.AddTransient<IIntersectionService, IntersectionService>();

            return services;
        }

        public static IServiceCollection RegisterDomainLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            // Intersector instances are globally injected as transient.
            // Doing so, you generally don't care about multithreading and memory leaks.
            // E.g. if in the future we decide to add an extra IIntersectorRegistry instance.
            services.AddTransient<IIntersector, CubesIntersector>();
            services.AddTransient<IIntersector, SpheresIntersector>();
            services.AddTransient<IIntersector, CylindersIntersector>();

            // IntersectorRegistry being a Singleton means different GetIntersector calls will reuse the
            // same instance of the registry, what in turn means its dependencies (specific intersectors)
            // will be reused on different GetIntersector calls (despite intersectors being injected as transients).
            services.AddSingleton<IIntersectorRegistry, IntersectorRegistry>();

            return services;
        }

        public static IServiceCollection RegisterDataLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            return services;
        }
    }
}
