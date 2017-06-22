using Microsoft.Practices.Unity;
using System;

namespace PrismSample.Infrastructure
{
    public static class UnityExtensions
    {
        public static void RegisterTypeForNavigation<T>(this IUnityContainer container)
        {
            container.RegisterType(typeof(Object), typeof(T), typeof(T).Name);
        }
    }
}
