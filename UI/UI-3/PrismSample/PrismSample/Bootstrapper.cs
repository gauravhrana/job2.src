using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;
using PrismSample.Controls;
using PrismSample.Navigation;
using PrismSample.Views;
using System.Windows;
using PrismSample.TTT;

namespace PrismSample
{
    public class Bootstrapper: UnityBootstrapper
    {

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(ControlsModule));
            catalog.AddModule(typeof(NavigationModule));
            catalog.AddModule(typeof(TTTModule));

            return catalog;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

    }

    //public static class UnityExtensions
    //{
    //    public static void RegisterTypeForNavigation<T>(this IUnityContainer container, string name)
    //    {
    //        container.RegisterType(typeof(object), typeof(T), name);
    //    }
    //}

}
