using Capluga.Models;
using Capluga.Services.Implementations;
using Capluga.Services.Interfaces;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace Capluga
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registra tus tipos dentro de Unity
            container.RegisterType<IUserService, UserService>();

            // Si tienes otras dependencias, regístralas aquí
            container.RegisterType<CaplugaDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}