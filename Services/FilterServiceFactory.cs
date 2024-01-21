using System;
using System.Collections.Generic;
using QueryCraft.Intrefaces;

namespace QueryCraft.Services
{
    public class FilterServiceFactory : IFilterServiceFactory
    {
        private readonly Dictionary<Type, object> Services;
        public FilterServiceFactory()
        {
            Services = new Dictionary<Type, object>();
        }

        public IFilterService<T> CreateFilterService<T>() where T : class
        {
            var serviceType = typeof(T);

            if (!Services.TryGetValue(serviceType, out var service))
            {
                service = new FilterService<T>();
                Services.Add(serviceType, service);
            }

            return (IFilterService<T>)service;
        }
    }
}
