using Microsoft.Extensions.DependencyInjection;
using QueryCraft.Convertors;
using QueryCraft.Interfaces;
using QueryCraft.Parsing;
using QueryCraft.TypeConversations;
using System;

namespace QueryCraft.MVC
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection RegisterQueryCraft(this IServiceCollection services, Action<QueryCraftOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var options = new QueryCraftOptions();
            configureOptions(options);

            RegisterTypeConverter(services, options);
            RegisterOperatorConverter(services, options);
            services.AddScoped<IParser, BodyParser>();
            return services;
        }

        private static void RegisterTypeConverter(this IServiceCollection services, QueryCraftOptions options)
        {
            if (options != null && options.ConverterTypes !=  null)
            {
                services.AddScoped<ITypeConverter>(opt => new TypeConverter(options.ConverterTypes));
            }
            else 
            {
                services.AddScoped<ITypeConverter, TypeConverter>();
            }
        }

        private static void RegisterOperatorConverter(this IServiceCollection services, QueryCraftOptions options)
        {
            if (options != null && options.ConverterOperators != null)
            {
                services.AddScoped<IOperatorConverter>(opt => new OperatorConverter(opt.GetRequiredService<ITypeConverter>(), options.ConverterOperators));
            }
            else
            {
                services.AddScoped<IOperatorConverter, OperatorConverter>();
            }
        }
    }
}
