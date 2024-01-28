using Microsoft.Extensions.DependencyInjection;
using QueryCraft.Convertors;
using QueryCraft.Interfaces;
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
            return services;
        }

        private static void RegisterTypeConverter(this IServiceCollection services, QueryCraftOptions options)
        {
            if (options != null && options.ConverterTypes !=  null)
            {
                services.AddTransient<ITypeConverter>(opt => new TypeConverter(options.ConverterTypes));
            }
            else 
            {
                services.AddTransient<ITypeConverter, TypeConverter>();
            }
        }

        private static void RegisterOperatorConverter(this IServiceCollection services, QueryCraftOptions options)
        {
            if (options != null && options.ConverterOperators != null)
            {
                services.AddTransient<IOperatorConverter>(opt => new OperatorConverter(options.ConverterOperators));
            }
            else
            {
                services.AddTransient<IOperatorConverter, OperatorConverter>();
            }
        }
    }
}
