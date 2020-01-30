using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orders.Api.Configuration
{
    public static class MappingConfig
    {
        const string PROJECT_PREFIX = "Orders";

        public static void AddMapping(this IServiceCollection services)
        {
            var loadedProfiles = RetrieveProfiles().ToArray();

            services.AddAutoMapper(loadedProfiles);
        }

        private static List<Type> RetrieveProfiles()
        {
            var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Where(a => a.Name.StartsWith(PROJECT_PREFIX))
                .Select(an => Assembly.Load(an));

            var loadedProfiles = ExtractProfiles(assemblies);
            return loadedProfiles;
        }

        private static List<Type> ExtractProfiles(IEnumerable<Assembly> assemblies)
        {
            var profiles = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var assemblyProfiles = assembly.ExportedTypes;
                profiles.AddRange(assemblyProfiles);
            }
            return profiles;
        }
    }
}
